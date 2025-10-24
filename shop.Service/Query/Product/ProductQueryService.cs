using Microsoft.EntityFrameworkCore;
using shop.Data.ApplicationContext;
using System;

namespace shop.Service.Query
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IApplicationContext _Context;

        public ProductQueryService(IApplicationContext Context)
        {
            _Context = Context;
        }
        // Lazy Loading → داده‌ها وقتی لازم شدند جداگانه query می‌زنند.
        public async Task<ProductQueryDto?> GetProductById(int productId)
        {
            var product = await _Context.Set<shop.Core.Domain.Product.Product>().Where(s => s.Deleted == false)
                .FirstOrDefaultAsync(f => f.Id == productId);

            var model = product.Map();
            if (model == null)
                return null;

            return model;
        }
        //The Best - The Better If Use Auto Mapper projection Dto On SQL
        //Projection (Select DTO) → فقط ستون‌های مورد نیاز برای DTO انتخاب می‌شوند و مستقیم map می‌شن.
        //public async Task<ProductQueryDto?> GetProductById(int productId)
        //{
        //    var model = await _Context.Set<shop.Core.Domain.Product.Product>()
        //        .Where(p => p.Deleted == false && p.Id == productId)
        //        .Select(p => new ProductQueryDto
        //        {
        //            Title = p.Name,
        //            ImageName = p.ImageName,
        //            Description = p.Description,

        //            // Categories
        //            Category = p.ProductCategories
        //                .Select(pc => new ProductCategoryDto
        //                {
        //                    ProductID = pc.ProductID,
        //                    CategoryID = pc.CategoryID,
        //                    CategoryName = pc.Category.Name
        //                }).ToList(),

        //            // Images
        //            Images = p.ProductPictures
        //                .Select(img => new ProductImageDto
        //                {
        //                    ProductID = img.ProductID,
        //                    PictureID = img.PictureID
        //                }).ToList(),

        //            // Specifications
        //            Specifications = p.Specifications
        //                .Select(spec => new ProductSpecificationDto
        //                {
        //                    Key = spec.Name,
        //                    Value = spec.Value
        //                }).ToList()
        //        })
        //        .AsNoTracking()  // چون read-only هست
        //        .FirstOrDefaultAsync();

        //    return model;
        //}
        public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams request)
        {
            var @params = request;
            var result = _Context.Set<shop.Core.Domain.Product.Product>().OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(r => r.Name.Contains(@params.Title));

            if (@params.Id != null)
                result = result.Where(r => r.Id == @params.Id);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new ProductFilterResult()
            {
                Data = result.Skip(skip).Take(@params.Take).Select(s => s.MapListData()).ToList(),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
