namespace shop.Core.Commons
{
    public abstract class BaseEntity : Entity
    {
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;

    }
}