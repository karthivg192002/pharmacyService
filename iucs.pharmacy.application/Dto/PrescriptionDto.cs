using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.application.Dto
{
    public class PrescriptionDto : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string DoctorName { get; set; } = null!;
        public string DoctorRegistrationNo { get; set; } = null!;
        public string? HospitalName { get; set; }
        public DateTime? PrescriptionDate { get; set; }
        public string? PrescriptionImagePath { get; set; }
    }

    public class PrescriptionItemDto : BaseEntity
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public string? Duration { get; set; }
        public decimal QuantityPrescribed { get; set; }
    }
}
