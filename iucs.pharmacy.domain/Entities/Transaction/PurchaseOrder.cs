using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using iucs.pharmacy.domain.Entities.Masters;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class PurchaseOrderBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid SupplierId { get; set; }
        public string OrderNo { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public string? Remarks { get; set; }
    }
    
    public class PurchaseOrder : PurchaseOrderBase
    {
        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    }
}
