using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.CategoryCommand;
using shop.Service.Query;

namespace shop.Web.Controllers.Category
{
    public class CategoryController : ShopController
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryQueryService _categoryQueryService;
        public CategoryController(ICategoryService categoryService, CategoryQueryService categoryQueryService)
        {
            _categoryService = categoryService;
            _categoryQueryService = categoryQueryService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ApiResult<List<CategoryQueryDto>>> GetAllCategories()
        {
            var result = await _categoryQueryService.GetAllCategory();
            return QueryResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryQueryDto>> GetCategoryById(int id)
        {
            var result = await _categoryQueryService.GetbyId(id);
            return QueryResult(result);
        }

        [HttpGet("getChild/{parentId}")]
        public async Task<ApiResult<List<ChildCategoriesDto>>> GetCategoriesByParentId(int parentId)
        {
            var result = await _categoryQueryService.GetByParentId(parentId);
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ApiResult> CreateCategory(CreateCategoryDto CreateCategoryDto)
        {
            var result = await _categoryService.AddCategory(CreateCategoryDto);
            return CreatedResult(result,null);
        }

        [HttpPost("AddChild(1 Child)")]
        public async Task<ApiResult> CreateCategory(CreateChildCategoryDto CreateChildCategoryDto)
        {
            var result = await _categoryService.AddChildCategory(CreateChildCategoryDto);
            return CreatedResult(result,null);
        }

        [HttpPut]
        public async Task<ApiResult> EditCategory(EditCategoryDto EditCategoryDto)
        {
            var result = await _categoryService.UpdateCategory(EditCategoryDto);
            return CommandResult(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ApiResult> RemoveCategory(int categoryId)
        {
            var result = await _categoryService.RemoveCategory(categoryId);
            return CommandResult(result);
        }


    }
}
