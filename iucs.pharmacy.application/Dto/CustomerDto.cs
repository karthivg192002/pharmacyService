using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.application.Dto
{
    public class CustomerDto : BaseEntity
    {
        public string? CustomerCode { get; set; }
        public string FullName { get; set; } = default!;
        public string? Mobile { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
    }
}
