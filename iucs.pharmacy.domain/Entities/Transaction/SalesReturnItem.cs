using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class SalesReturnItemBase : BaseEntity
    {
        public Guid SalesReturnId { get; set; }
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal RefundAmount { get; set; }
    }

    public class SalesReturnItem : SalesReturnItemBase
    {
    }
}
