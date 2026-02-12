using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.application.Dto
{
    public class ManufacturerDto : BaseEntity
    {
        public string ManufacturerName { get; set; } = default!;
        public string? ShortName { get; set; } = default!;
    }
}
