using shop.Core.Domain.Category;

namespace shop.Service.Query
{
    public static class CategoryQueryMapper
    {

        public static CategoryQueryDto Map(this Category category)
        {
            if (category == null)
                return null;

            return new CategoryQueryDto()
            {
                Name = category.Name,
                ChildCount = category.ChildCategories.Count,
                ProductCount = category.productCategories.Count,
                Id = category.Id,
                CreationDate = category.CreateON,
                ChildCategories = category.ChildCategories.ToList().MapChildren(),
            };
        }

        public static List<CategoryQueryDto> Map(this List<Category> categories)
        {
            var model = new List<CategoryQueryDto>();

            categories.ForEach(category =>
            {
                model.Add(new CategoryQueryDto()
                {
                    Name = category.Name,
                    Id = category.Id,
                    ChildCount = category.ChildCategories.Count,
                    ProductCount = category.productCategories.Count,
                    CreationDate = category.CreateON,
                    ChildCategories = category.ChildCategories.ToList().MapChildren()
                }) ;
            });
            return model;
        }

        public static List<ChildCategoriesDto> MapChildren(this List<Category> children)
        {
            var model = new List<ChildCategoriesDto>();

            children.ForEach(c =>
            {
                model.Add(new ChildCategoriesDto()
                {
                    Name = c.Name,
                    Id = c.Id,
                    CreationDate = c.CreateON,
                    ChildCount = c.ChildCategories.Count,
                    ProductCount = c.productCategories.Count,
                    ParentId = (int?)c.ParentID,
                    ParentName = c.PatentCategory.Name

                });
            });
            return model;
        }


    }
}
