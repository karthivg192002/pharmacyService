using iucs.pharmacy.application.Services.TenantService;
using iucs.pharmacy.domain.Data.Tenant;
using Microsoft.AspNetCore.Http;

namespace iucs.pharmacy.application.Middleware
{
    public class TenantResolveMiddleware
    {
        private const string TenantHeader = "x-tenant-code";
        private const string EnvHeader = "x-env";

        private readonly RequestDelegate _next;

        public TenantResolveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService, ITenantContext tenantContext)
        {
            if (HttpMethods.IsOptions(context.Request.Method))
            {
                await _next(context);
                return;
            }

            var path = context.Request.Path.Value?.ToLower();

            if (path!.StartsWith("/scalar") || path.StartsWith("/openapi") || path.StartsWith("/swagger") || path.StartsWith("/health"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(TenantHeader, out var tenantCode))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("x-tenant-code header is missing");
                return;
            }

            if (!context.Request.Headers.TryGetValue(EnvHeader, out var env))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("x-env header is missing");
                return;
            }

            try
            {
                var tenant = await tenantService.ResolveAsync(tenantCode.ToString(), env.ToString(), "pharmacy");

                if (tenant == null || string.IsNullOrWhiteSpace(tenant.Connection))
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Tenant not found or invalid configuration");
                    return;
                }

                tenantContext.SetTenant(tenant.TenantCode, tenant.Connection, tenant.DbProvider.ToLowerInvariant(), tenant.Environment.ToLowerInvariant());

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"Tenant resolution failed: {ex.Message}");
            }
        }
    }
}
