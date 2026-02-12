using iucs.pharmacy.domain.Entities.Common;

namespace iucs.pharmacy.domain.Entities.Masters
{
    public class ManufacturerBase : BaseEntity
    {
        public string ManufacturerName { get; set; } = default!;
        public string? ShortName { get; set; } = default!;
    }
    public class Manufacturer : ManufacturerBase
    {
        
    }
}
