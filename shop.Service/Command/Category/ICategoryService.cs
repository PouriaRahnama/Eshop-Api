using shop.Service.DTOs.CategoryCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command;
public interface ICategoryService
{
    Task<OperationResult> AddCategory(CreateCategoryDto CreateCategoryDto);
    Task<OperationResult> UpdateCategory(EditCategoryDto EditCategoryDto);
    Task<OperationResult> AddChildCategory(CreateChildCategoryDto CreateChildCategoryDto);
    Task<OperationResult> RemoveCategory(int Id);
}



