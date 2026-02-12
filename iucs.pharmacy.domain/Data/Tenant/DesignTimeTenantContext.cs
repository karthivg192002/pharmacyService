namespace iucs.pharmacy.domain.Data.Tenant
{
    public class DesignTimeTenantContext : ITenantContext
    {
        public string TenantCode { get; private set; } = "DESIGN";
        public string ConnectionString { get; private set; }
        public string DbProvider { get; private set; } = "npgsql";
        public string Environment { get; private set; } = "design";

        public DesignTimeTenantContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void SetTenant(string tenantCode, string connectionString, string dbProvider, string environment)
        {
            TenantCode = tenantCode;
            ConnectionString = connectionString;
            DbProvider = dbProvider;
            Environment = environment;
        }
    }
}
