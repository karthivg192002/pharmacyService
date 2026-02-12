using System.Net.Http.Json;
using iucs.pharmacy.application.Dto;
using iucs.pharmacy.domain.Data;
using iucs.pharmacy.domain.Data.Tenant;
using Microsoft.EntityFrameworkCore;

namespace iucs.pharmacy.application.Services.TenantService
{
    public interface ITenantService
    {
        Task<TenantResponseDto> MigrateAsync(string applicationCode);
        Task<TenantResolveDto> ResolveAsync(string tenantCode, string env, string applicationCode);
    }

    public class TenantService : ITenantService
    {
        private readonly ITenantContext _tenantContext;
        private readonly HttpClient _http;
        private readonly AppDbContext _dbContext;

        public TenantService(ITenantContext tenantContext, HttpClient http, AppDbContext dbContext)
        {
            _tenantContext = tenantContext;
            _http = http;
            _dbContext = dbContext;
        }

        public async Task<TenantResponseDto> MigrateAsync(string applicationCode)
        {
            var response = new TenantResponseDto();

            try
            {
                if (string.IsNullOrWhiteSpace(_tenantContext.ConnectionString))
                {
                    response.Success = false;
                    response.Message = "Tenant not resolved";
                    response.Data = "";
                }

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

                if (_tenantContext.DbProvider == "postgressql")
                {
                    optionsBuilder.UseNpgsql(_tenantContext.ConnectionString);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Unsupported DB provider";
                    response.Data = "";
                }

                using var db = new AppDbContext(optionsBuilder.Options, _tenantContext);
                await db.Database.MigrateAsync();

                await UpdateMigrationInTenant(_tenantContext.TenantCode, _tenantContext.Environment, applicationCode);

                response.Success = true;
                response.Message = $"Migration Done for the {_tenantContext.TenantCode} - {applicationCode}";
                response.Data = "";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Unsupported DB provider {ex}";
                response.Data = $"{ex}";
            }
            return response;
        }

        public async Task<TenantResolveDto> ResolveAsync(string tenantCode, string env, string applicationCode)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"tenant/resolve/{applicationCode}");

            request.Headers.Add("x-tenant-code", tenantCode);
            request.Headers.Add("x-env", env);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TenantResolveDto>()
                   ?? throw new Exception("Tenant not found");
        }

        public async Task<bool> UpdateMigrationInTenant(string tenantCode, string env, string applicationCode)
        {
            using var request = new HttpRequestMessage(HttpMethod.Put, $"tenant/update/migration/{applicationCode}");

            request.Headers.Add("x-tenant-code", tenantCode);
            request.Headers.Add("x-env", env);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
