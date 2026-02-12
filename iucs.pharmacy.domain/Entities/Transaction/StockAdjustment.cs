using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class StockAdjustmentBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string? Reason { get; set; }
        public Guid? ApprovedBy { get; set; }
    }

    public class StockAdjustment : StockAdjustmentBase
    {
        public ICollection<StockAdjustmentItem> Items { get; set; } = new List<StockAdjustmentItem>();
    }
}
