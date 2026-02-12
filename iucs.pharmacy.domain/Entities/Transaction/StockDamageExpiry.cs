using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class StockDamageExpiryBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public StockDamageReason Reason { get; set; }
        public DateTime RecordedDate { get; set; }
        public Guid? ApprovedBy { get; set; }
    }

    public class StockDamageExpiry : StockDamageExpiryBase 
    {
    }
}
