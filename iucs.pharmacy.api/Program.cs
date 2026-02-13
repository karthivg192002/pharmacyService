using iucs.pharmacy.application.Mapper;
using iucs.pharmacy.application.Middleware;
using iucs.pharmacy.application.Services;
using iucs.pharmacy.application.Services.CommonCurdService;
using iucs.pharmacy.application.Services.MastersCurdService;
using iucs.pharmacy.application.Services.TenantService;
using iucs.pharmacy.domain.Data;
using iucs.pharmacy.domain.Data.Tenant;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
builder.Services.AddScoped<ManufacturerService>();
builder.Services.AddScoped<MedicineCategoryService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<PrescriptionService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<IPurchaseInvoiceService, PurchaseInvoiceService>();
builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddHttpClient<ITenantService, TenantService>(c =>
{
    c.BaseAddress = new Uri("https://tenantapi.edulanz.com/api/");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.Title = "Pharmacy Api";
        options.Theme = ScalarTheme.Kepler;
        options.ShowSidebar = true;
    });
}

app.UseHttpsRedirection();


app.UseCors("AllowOrigin");

app.UseMiddleware<TenantResolveMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
