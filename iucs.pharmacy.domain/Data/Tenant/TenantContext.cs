namespace iucs.pharmacy.domain.Data.Tenant
{
    public interface ITenantContext
    {
        string TenantCode { get; }
        string ConnectionString { get; }
        string DbProvider { get; }
        string Environment { get; }

        void SetTenant(string tenantCode, string connectionString, string dbProvider, string environment);
    }

    public class TenantContext : ITenantContext
    {
        public string TenantCode { get; private set; } = default!;
        public string ConnectionString { get; private set; } = default!;
        public string DbProvider { get; private set; } = default!;
        public string Environment { get; private set; } = default!;

        public void SetTenant(string tenantCode, string connectionString, string dbProvider, string environment)
        {
            TenantCode = tenantCode;
            ConnectionString = connectionString;
            DbProvider = dbProvider;
            Environment = environment;
        }
    }
}
