using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class ControlledDrugRegisterBase : AuditEntity
    {
        public Guid BranchId { get; set; }
        public Guid MedicineId { get; set; }
        public Guid BatchId { get; set; }
        public Guid SalesInvoiceId { get; set; }
        public Guid CustomerId { get; set; }
        public string DoctorName { get; set; } = null!;
        public string DoctorRegistrationNo { get; set; } = null!;
        public decimal Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid PerformedBy { get; set; }
    }

    public class ControlledDrugRegister : ControlledDrugRegisterBase
    {
    }
}
