using shop.Core.Domain.Category;
using shop.Core.Domain.Product;
using shop.Data.Repository;
using shop.Service.DTOs.CategoryCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command;
public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;
    private readonly IRepository<ProductCategory> _ProductCategoryRepository;
    public CategoryService(IRepository<Category> Repository, IRepository<ProductCategory> ProductCategoryRepository)
    {
        _repository = Repository;
        _ProductCategoryRepository = ProductCategoryRepository;
    }

    public async Task<OperationResult> AddCategory(CreateCategoryDto CreateCategoryDto)
    {
        var category = new Category()
        {
            Name = CreateCategoryDto.Name
        };
        await _repository.AddAsync(category);
        return OperationResult.Success();
    }
    public async Task<OperationResult> AddChildCategory(CreateChildCategoryDto CreateChildCategoryDto)
    {
        var parentCategory =await _repository.GetEntity(c => c.Id == CreateChildCategoryDto.ParentId);
        if(parentCategory.Deleted == true)
            return OperationResult.Error("!این دسته حذف شده است");

        if (parentCategory.ParentID != null)
                return OperationResult.Error("1 child in Exist!");

        var category = new Category()
        {
            Name = CreateChildCategoryDto.Name,
            ParentID = CreateChildCategoryDto.ParentId
        };
        await _repository.AddAsync(category);
        return OperationResult.Success();
    }

    public async Task<OperationResult> RemoveCategory(int Id)
    {
        var category = await _repository.FindByIdAsync(Id);
        if (category == null)
            return OperationResult.NotFound("!دسته بندی پیدا نشد");

        var IsExistProduct =await _ProductCategoryRepository.GetEntity(c => c.CategoryID == category.Id);
        if (IsExistProduct != null)
            return OperationResult.Error("!محصولی با این دسته بندی وجود دارد ");

        var ChildCategories = _repository.Get(c => c.ParentID == Id).ToList();
        if (ChildCategories != null)
            foreach (var childCategory in ChildCategories)
            {
                childCategory.Deleted = true;
                _repository.Update(childCategory);
            }

        category.Deleted = true;
        _repository.Update(category);
        return OperationResult.Success();
    }

    public async Task<OperationResult> UpdateCategory(EditCategoryDto EditCategoryDto)
    {
        var category = await _repository.FindByIdAsync(EditCategoryDto.Id);
        if (category == null)
            return OperationResult.NotFound();

        category.Name = EditCategoryDto.Name;
        category.UpdateON = DateTime.Now;

        _repository.Update(category);
        return OperationResult.Success();

    }
}

