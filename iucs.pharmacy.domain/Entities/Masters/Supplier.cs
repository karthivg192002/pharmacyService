using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Masters
{
    public class SupplierBase : BaseEntity
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

    public class Supplier : SupplierBase
    {

    }
}
