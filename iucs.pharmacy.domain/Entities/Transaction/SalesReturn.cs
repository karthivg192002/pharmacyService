using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class SalesReturnBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid SalesInvoiceId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Reason { get; set; }
        public Guid? ApprovedBy { get; set; }
    }

    public class SalesReturn : SalesReturnBase
    {
        public ICollection<SalesReturnItem> Items { get; set; } = new List<SalesReturnItem>();
    }
}
