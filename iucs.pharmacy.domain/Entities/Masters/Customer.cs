using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Masters
{
    public class CustomerBase : AuditEntity
    {
        public string? CustomerCode { get; set; }
        public string FullName { get; set; } = default!;
        public string? Mobile { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
    }

    public class Customer : CustomerBase
    {

    }
}
