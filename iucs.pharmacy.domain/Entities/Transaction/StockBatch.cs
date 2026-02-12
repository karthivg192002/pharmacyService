using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using iucs.pharmacy.domain.Entities.Masters;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class StockBatchBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid MedicineId { get; set; }
        public string BatchNo { get; set; } = default!;
        public DateTime? ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal Mrp { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal GstPercent { get; set; }
        public decimal OpeningQuantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public bool IsBlocked { get; set; }
        public Guid CreatedFromPurchaseItemId { get; set; }
    }

    public class StockBatch : StockBatchBase
    {
        public Medicine? Medicine { get; set; }
    }
}
