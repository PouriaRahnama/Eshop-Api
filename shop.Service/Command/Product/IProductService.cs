using shop.Service.DTOs.ProductCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public interface IProductService
    {
        Task<OperationResult> AddProductCategory(AddProductCategoryDto AddProductCategoryDto);
        Task<OperationResult> AddProductPicture(AddProductPictureDto AddProductPictureDto);
        Task<OperationResult> AddProductSpecification(AddProductSpecificationDto AddProductSpecificationDto);
        Task<OperationResult> AddProduct(CreateProductDto CreateProductDto);
        Task<OperationResult> AddPicture(CreatePictureDto CreatePictureDto);
        Task<OperationResult> UpdateProduct(EditProductDto EditProductDto);
        Task<OperationResult> RemovePicture(RemovePictureDto RemovePicture);
        Task<OperationResult> RemoveProductCategory(RemoveProductCategoryDto RemoveProductCategoryDto);
        Task<OperationResult> RemoveProductPicture(RemoveProductPictureDto RemoveProductPicture);
        Task<OperationResult> RemoveProductSpecification(RemoveProductSpecificationDto RemoveProductSpecification);

    }
}
