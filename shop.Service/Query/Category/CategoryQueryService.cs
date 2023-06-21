using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Category;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public class CategoryQueryService
    {
        private readonly IApplicationContext _context;
        public CategoryQueryService(IApplicationContext context)
        {
            _context = context;
        }
        public async Task<CategoryQueryDto> GetbyId(int CategoryId)
        {
            var result = await _context.Set<Category>().Where(c => c.Deleted == false)
                .FirstOrDefaultAsync(f => f.Id == CategoryId);

            return result.Map();
        }
        public async Task<List<ChildCategoriesDto>> GetByParentId(int parentId)
        {
            var result = await _context.Set<Category>()
                .Where(r => r.ParentID == parentId)
                .Select(r => new ChildCategoriesDto()
                {
                    Id = r.Id,
                    Name = r.Name,
                    ParentId = parentId,
                    ChildCount = r.ChildCategories.Count,
                    ParentName = r.PatentCategory.Name

                }).ToListAsync();

            if (result is null)
                throw new ArgumentNullException("Not Found");

            return result;
        }
        public async Task<List<CategoryQueryDto>> GetAllCategory()
        {
            var model = await _context.Set<Category>().Where(c =>c.ParentID == null && c.Deleted == false).OrderByDescending(d => d.Id).ToListAsync();
            return model.Map();
        }
    }
}
