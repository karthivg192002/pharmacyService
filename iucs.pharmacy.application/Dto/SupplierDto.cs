using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.application.Dto
{
    public class SupplierDto : BaseEntity
    {
        public string SupplierCode { get; set; } = default!;
        public string SupplierName { get; set; } = default!;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Gstin { get; set; }
        public string? DrugLicenseNo { get; set; }
    }
}
