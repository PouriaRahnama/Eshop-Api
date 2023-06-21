namespace shop.Core.Commons
{
    public class Entity
    {
        public bool Deleted { get; set; } = false;
        public DateTime CreateON { get; set; } = DateTime.Now;
        public DateTime? UpdateON { get; set; }
    }
}
