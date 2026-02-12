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
    public class PurchaseInvoiceBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetAmount { get; set; }
    }

    public class PurchaseInvoice : PurchaseInvoiceBase
    {
        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}
