using Microsoft.AspNetCore.Mvc;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.CategoryCommand;
using shop.Service.Extension.Util;
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

        [HttpGet]
        public async Task<ActionResult<List<CategoryQueryDto>>> GetAllCategories()
        {
            var result = await _categoryQueryService.GetAllCategory();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryQueryDto>> GetCategoryById(int id)
        {
            var result = await _categoryQueryService.GetbyId(id);
            return Ok(result);
        }

        [HttpGet("getChild/{parentId}")]
        public async Task<ActionResult<List<ChildCategoriesDto>>> GetCategoriesByParentId(int parentId)
        {
            var result = await _categoryQueryService.GetByParentId(parentId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto CreateCategoryDto)
        {
            var result = await _categoryService.AddCategory(CreateCategoryDto);
            if (result.Status == OperationResultStatus.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpPost("AddChild(1 Child)")]
        public async Task<IActionResult> CreateCategory(CreateChildCategoryDto CreateChildCategoryDto)
        {
            var result = await _categoryService.AddChildCategory(CreateChildCategoryDto);
            if (result.Status == OperationResultStatus.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory(EditCategoryDto EditCategoryDto)
        {
            var result = await _categoryService.UpdateCategory(EditCategoryDto);
            if (result.Status == OperationResultStatus.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var result = await _categoryService.RemoveCategory(categoryId);
            if (result.Status == OperationResultStatus.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }


    }
}
