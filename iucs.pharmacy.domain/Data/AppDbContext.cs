using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Data.Tenant;
using iucs.pharmacy.domain.Entities.Masters;
using iucs.pharmacy.domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;

namespace iucs.pharmacy.domain.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ITenantContext _tenantContext;

        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantContext tenantContext) :
            base(options)
        {
            _tenantContext = tenantContext;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            if (string.IsNullOrWhiteSpace(_tenantContext.ConnectionString))
                throw new InvalidOperationException("Tenant connection string not resolved");

            optionsBuilder.UseNpgsql(_tenantContext.ConnectionString);
        }

        #region DbSets

        #region Masters

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<MedicineCategory> MedicineCategory { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItem { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        #endregion

        #region Transaction

        public DbSet<Alert> Alert { get; set; }
        public DbSet<ControlledDrugRegister> ControlledDrugRegister { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoice { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItem { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItem { get; set; }
        public DbSet<SalesInvoice> SalesInvoice { get; set; }
        public DbSet<SalesInvoiceItem> SalesInvoiceItem { get; set; }
        public DbSet<SalesPayment> SalesPayment { get; set; }
        public DbSet<SalesReturn> SalesReturn { get; set; }
        public DbSet<SalesReturnItem> SalesReturnItem { get; set; }
        public DbSet<StockAdjustment> StockAdjustment { get; set; }
        public DbSet<StockAdjustmentItem> StockAdjustmentItem { get; set; }
        public DbSet<StockBatch> StockBatch { get; set; }
        public DbSet<StockDamageExpiry> StockDamageExpiry { get; set; }
        public DbSet<StockLedger> StockLedger { get; set; }
        public DbSet<StockTransfer> StockTransfer { get; set; }
        public DbSet<StockTransferItem> StockTransferItem { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(mapper.TranslateMemberName(entity.GetTableName()!));

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(mapper.TranslateMemberName(property.Name));
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(mapper.TranslateMemberName(key.GetName()!));
                }

                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.SetConstraintName(mapper.TranslateMemberName(fk.GetConstraintName()!));
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(mapper.TranslateMemberName(index.GetDatabaseName()!));
                }
            }
        }
    }
}
