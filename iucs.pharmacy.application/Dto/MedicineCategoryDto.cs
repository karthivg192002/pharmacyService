using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.application.Dto
{
    public class MedicineCategoryDto : BaseEntity
    {
        public string CategoryName { get; set; } = default!;
    }
}
