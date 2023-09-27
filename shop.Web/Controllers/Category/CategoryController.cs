using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.CategoryCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;

namespace shop.Web.Controllers.Category;

[PermissionChecker(Permission.Category_Management)]

public class CategoryController : ShopController
{
    private readonly ICategoryService _categoryService;
    private readonly ICategoryQueryService _categoryQueryService;
    public CategoryController(ICategoryService categoryService, ICategoryQueryService categoryQueryService)
    {
        _categoryService = categoryService;
        _categoryQueryService = categoryQueryService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<CategoryQueryDto>>> GetAllCategories()
    {
        var result = await _categoryQueryService.GetAllCategory();
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{Id}")]
    public async Task<ApiResult<CategoryQueryDto>> GetCategoryById(int Id)
    {
        var result = await _categoryQueryService.GetbyId(Id);
        return QueryResult(result);
    }

    [HttpGet("Child/{parentId}")]
    public async Task<ApiResult<List<ChildCategoriesDto>>> GetCategoriesByParentId(int parentId)
    {
        var result = await _categoryQueryService.GetByParentId(parentId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> CreateCategory(CreateCategoryDto CreateCategoryDto)
    {
        var result = await _categoryService.AddCategory(CreateCategoryDto);
        return CreatedResult(result, null);
    }

    [HttpPost("Child")] //(1 Child)
    public async Task<ApiResult> CreateCategory(CreateChildCategoryDto CreateChildCategoryDto)
    {
        var result = await _categoryService.AddChildCategory(CreateChildCategoryDto);
        return CreatedResult(result, null);
    }

    [HttpPut]
    public async Task<ApiResult> EditCategory(EditCategoryDto EditCategoryDto)
    {
        var result = await _categoryService.UpdateCategory(EditCategoryDto);
        return CommandResult(result);
    }

    [HttpDelete("{Id}")]
    public async Task<ApiResult> RemoveCategory(int Id)
    {
        var result = await _categoryService.RemoveCategory(Id);
        return CommandResult(result);
    }
}

