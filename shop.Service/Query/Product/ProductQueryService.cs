using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Product;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public class ProductQueryService
    {
        private readonly IApplicationContext _Context;

        public ProductQueryService(IApplicationContext Context)
        {
            _Context = Context;
        }

        public async Task<ProductQueryDto?> GetProductById(int productId)
        {
            var product = await _Context.Set<Product>().Where(s => s.Deleted == false)
                .FirstOrDefaultAsync(f => f.Id == productId);

            var model = product.Map();
            if (model == null)
                return null;

            return model;
        }

        public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParams request)
        {
            var @params = request;
            var result = _Context.Set<Product>().OrderByDescending(d => d.Id).AsQueryable();

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
