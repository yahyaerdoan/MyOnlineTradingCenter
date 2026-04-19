using MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities;

namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
