using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Masters
{
    public class PrescriptionBase : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string DoctorName { get; set; } = null!;
        public string DoctorRegistrationNo { get; set; } = null!;
        public string? HospitalName { get; set; }
        public DateTime? PrescriptionDate { get; set; }
        public string? PrescriptionImagePath { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    
    public class Prescription : PrescriptionBase
    {
        public Customer? Customer { get; set; }
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}
