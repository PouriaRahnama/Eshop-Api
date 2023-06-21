namespace shop.Service.DTOs.CommonsCommand
{
    public class BaseItemDTO
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreateON { get; set; }
        public DateTime UpdateON { get; set; }
        public string LocalCreate { get; set; }
        public string LocalUpdate { get; set; }
    }
}
