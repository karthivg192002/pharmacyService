namespace iucs.pharmacy.domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
