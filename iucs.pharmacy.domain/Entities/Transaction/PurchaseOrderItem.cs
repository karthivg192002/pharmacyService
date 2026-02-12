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
    public class PurchaseOrderItemBase : BaseEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid MedicineId { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal? FreeQuantity { get; set; }
    }

    public class PurchaseOrderItem : PurchaseOrderItemBase
    {
        public PurchaseOrder? PurchaseOrder { get; set; }
        public Medicine? Medicine { get; set; }
    }
}
