using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class StockAdjustmentItemBase : BaseEntity
    {
        public Guid StockAdjustmentId { get; set; }
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public decimal SystemQty { get; set; }
        public decimal PhysicalQty { get; set; }
        public decimal DifferenceQty { get; set; }
    }

    public class StockAdjustmentItem : StockAdjustmentItemBase
    {
    }
}
