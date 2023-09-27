using shop.Service.Extension.Util;

namespace shop.Service.Query
{
    public class ProductQueryDto:BaseDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public List<ProductCategoryDto> Category { get; set; }
        public List<ProductImageDto> Images { get; set; }
        public List<ProductSpecificationDto> Specifications { get; set; }
    }
    public class ProductCategoryDto : BaseDto
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductSpecificationDto:BaseDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class ProductImageDto
    {
        public int ProductID { get; set; }
        public int PictureID { get; set; }
    }
    public class ProductFilterData : BaseDto
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
    }
    public class ProductFilterParams : BaseFilterParam
    {
        public string? Title { get; set; }
        public int? Id { get; set; }
    }
    public class ProductFilterResult : BaseFilter<ProductFilterData, ProductFilterParams>
    {

    }

}
