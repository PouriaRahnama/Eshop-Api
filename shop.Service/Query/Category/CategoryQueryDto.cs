namespace shop.Service.Query
{
    public class CategoryQueryDto : BaseDto  
    {
        public string Name { get; set; }
        public int ChildCount { get; set; }
        public int ProductCount { get; set; }
        public List<ChildCategoriesDto>? ChildCategories { get; set; }
    }
    public class ChildCategoriesDto : BaseDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public int ChildCount { get; set; }
        public int ProductCount { get; set; }

    }
}
