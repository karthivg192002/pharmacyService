using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Masters
{
    public class MedicineCategoryBase : BaseEntity
    {
        public string CategoryName { get; set; } = default!;
    }
    public class MedicineCategory : MedicineCategoryBase
    {

    }
}
