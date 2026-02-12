using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class SalesPaymentBase : AuditEntity
    {
        public Guid SalesInvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string? ReferenceNo { get; set; }
        public Guid? ReceivedBy { get; set; }
    }

    public class SalesPayment : SalesPaymentBase
    {
        public SalesInvoice? SalesInvoice { get; set; }
    }
}
