namespace shop.Service.DTOs.CategoryCommand
{
    public class CreateChildCategoryDto
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}
