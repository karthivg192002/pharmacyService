using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Masters
{
    public class PrescriptionItemBase : BaseEntity
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicineId { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public string? Duration { get; set; }
        public decimal QuantityPrescribed { get; set; }
    }

    public class PrescriptionItem : PrescriptionItemBase
    {
        public Prescription? Prescription { get; set; }
        public Medicine? Medicine { get; set; }
    }
}
