namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.CommonEntities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
