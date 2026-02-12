using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.application.Dto
{
    public class MedicineDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ManufacturerId { get; set; }
        public string MedicineCode { get; set; } = null!;
        public string MedicineName { get; set; } = null!;
        public string? GenericName { get; set; }
        public string DosageForm { get; set; } = null!;
        public string? Strength { get; set; }
        public string? PackSize { get; set; }
        public string? HsnCode { get; set; }
        public bool IsPrescriptionRequired { get; set; }
        public bool IsControlled { get; set; }
        public decimal DefaultGstPercent { get; set; }
        public MedicineScheduleType ScheduleType { get; set; }
        public bool IsActive { get; set; }
    }
}
