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
    public class SalesInvoiceBase : AuditEntity 
    {
        public Guid BranchId { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public DateTime InvoiceDate { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PrescriptionId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetAmount { get; set; }
        public SalesInvoiceStatus Status { get; set; }
    }

    public class SalesInvoice : SalesInvoiceBase
    {
        public Customer? Customer { get; set; }
        public Prescription? Prescription { get; set; }
        public ICollection<SalesInvoiceItem> Items { get; set; } = new List<SalesInvoiceItem>();
    }
}
