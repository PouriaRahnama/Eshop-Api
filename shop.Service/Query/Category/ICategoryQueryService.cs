using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public interface ICategoryQueryService
    {
        Task<CategoryQueryDto> GetbyId(int CategoryId);
        Task<List<ChildCategoriesDto>> GetByParentId(int parentId);
        Task<List<CategoryQueryDto>> GetAllCategory();
    }
}
