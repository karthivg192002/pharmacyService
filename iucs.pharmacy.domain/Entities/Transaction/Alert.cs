using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;
using static iucs.pharmacy.domain.Entities.Common.EnumsModel;

namespace iucs.pharmacy.domain.Entities.Transaction
{
    public class AlertBase : BaseEntity
    {
        public Guid BranchId { get; set; }
        public AlertType AlertType { get; set; }
        public Guid? ReferenceId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }

    public class Alert : AlertBase
    {
    }
}
