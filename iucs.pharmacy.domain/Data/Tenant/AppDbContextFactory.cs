using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace iucs.pharmacy.domain.Data.Tenant
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = Environment.GetEnvironmentVariable("SALESDESIGN");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Environment variable 'SALESDESIGN' is not set.");

            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("iucs.salesExecutive.api"));
            var tenantContext = new DesignTimeTenantContext(connectionString);

            return new AppDbContext(optionsBuilder.Options, tenantContext);
        }
    }
}
