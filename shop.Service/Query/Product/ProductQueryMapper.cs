namespace shop.Service.Query
{
    public static class ProductQueryMapper
    {
        public static ProductQueryDto? Map(this Core.Domain.Product.Product? product)
        {
            if (product == null)
                return null;
            return new()
            {
                Id = product.Id,
                CreationDate = product.CreateON,
                Description = product.Description,
                ImageName = product.ImageName,
                Title = product.Name,

                Specifications = product.Specifications.Where(s => s.Deleted == false).Select(s => new ProductSpecificationDto()
                {
                    Id = s.Id,
                    Value = s.Value,
                    Key = s.Name,
                    CreationDate = s.CreateON

                }).ToList(),

                Images = product.ProductPictures.Select(s => new ProductImageDto()
                {
                    ProductID = s.ProductID,
                    PictureID = s.PictureID,

                }).ToList(),

                Category = product.ProductCategories.Select(s => new ProductCategoryDto()
                {
                    Id = s.CategoryID,
                    CreationDate = s.CreateON,
                    ProductID = s.ProductID,
                    CategoryID = s.CategoryID,
                    CategoryName = s.Category.Name

                }).ToList(),

            };
        }

        public static ProductFilterData MapListData(this Core.Domain.Product.Product? product)
        {
            return new ProductFilterData()
            {
                Id = product.Id,
                CreationDate = product.CreateON,
                ImageName = product.ImageName,
                Title = product.Name
            };
        }

    }
}
