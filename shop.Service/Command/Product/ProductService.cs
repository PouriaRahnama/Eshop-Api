using shop.Core.Domain.Category;
using shop.Core.Domain.Product;
using shop.Data.Repository;
using shop.Service.DTOs.ProductCommand;
using shop.Service.Extension.FileUtil.Interfaces;
using shop.Service.Extension.Util;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace shop.Service.Command
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Picture> _PictureRepository;
        private readonly IRepository<ProductCategory> _ProductCategoryRepository;
        private readonly IRepository<ProductPicture> _ProductPictureRepository;
        private readonly IRepository<ProductSpecification> _ProductSpecificationRepository;
        private readonly IRepository<Category> _caegoryRepository;
        private readonly IFileService _fileService;

        public ProductService(IRepository<Product> Repository,
            IFileService fileService,
            IRepository<Picture> pictureRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<Category> caegoryRepository,
            IRepository<ProductPicture> productPictureRepository,
            IRepository<ProductSpecification> productSpecificationRepository)
        {
            _repository = Repository;
            _fileService = fileService;
            _PictureRepository = pictureRepository;
            _ProductCategoryRepository = productCategoryRepository;
            _caegoryRepository = caegoryRepository;
            _ProductPictureRepository = productPictureRepository;
            _ProductSpecificationRepository = productSpecificationRepository;
        }

        public async Task<OperationResult> AddPicture(CreatePictureDto CreatePictureDto)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(CreatePictureDto.ImageFile, Directories.ProductImages);

            var picture = new Picture()
            {
                ImageName = imageName
            };

            await _PictureRepository.AddAsync(picture);
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddProduct(CreateProductDto CreateProductDto)
        {
            var imageName = await _fileService.SaveFileAndGenerateName(CreateProductDto.ImageFile, Directories.ProductImages);
            var product = new Product()
            {
                Description = CreateProductDto.Description,
                Name = CreateProductDto.Title,
                ImageName = imageName
            };

            await _repository.AddAsync(product);
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddProductCategory(AddProductCategoryDto AddProductCategoryDto)
        {
            var product = _repository.FindByIdAsync(AddProductCategoryDto.ProductID);
            if (product == null)
                return OperationResult.NotFound();

            var category = _repository.FindByIdAsync(AddProductCategoryDto.CategoryID);
            if (category == null)
                return OperationResult.NotFound();

            var productCategory = new ProductCategory()
            {
                CategoryID = AddProductCategoryDto.CategoryID,
                ProductID = AddProductCategoryDto.ProductID
            };

            await _ProductCategoryRepository.AddAsync(productCategory);
            return OperationResult.Success();
        }

        public async Task<OperationResult> AddProductPicture(AddProductPictureDto AddProductPictureDto)
        {
            var product = await _repository.FindByIdAsync(AddProductPictureDto.ProductID);
            if (product == null)
                return OperationResult.NotFound("!محصول مورد نظر یافت نشد");

            var picture = _PictureRepository.FindByIdAsync(AddProductPictureDto.PictureID);
            if (picture == null)
                return OperationResult.NotFound("!عکس مورد نظر یافت نشد");

            var productPicure = new ProductPicture()
            {
                PictureID = AddProductPictureDto.PictureID,
                ProductID = AddProductPictureDto.ProductID
            };

            await _ProductPictureRepository.AddAsync(productPicure);
            return OperationResult.Success();
        }
        public async Task<OperationResult> AddProductSpecification(AddProductSpecificationDto AddProductSpecificationDto)
        {
            var product = await _repository.FindByIdAsync(AddProductSpecificationDto.ProductId);
            if (product == null)
                return OperationResult.NotFound("!محصول مورد نظر یافت نشد");

            var ProductSpecification = new ProductSpecification()
            {
                Value = AddProductSpecificationDto.Value,
                Name = AddProductSpecificationDto.Name,
                ProductId = AddProductSpecificationDto.ProductId
            };
            await _ProductSpecificationRepository.AddAsync(ProductSpecification);
            return OperationResult.Success();
        }

        public async Task<OperationResult> RemovePicture(RemovePictureDto RemovePicture)
        {
            var OldImageName = await _PictureRepository.FindByIdAsync(RemovePicture.PictureId);
            if (OldImageName == null)
                return OperationResult.NotFound("!عکس مورد نظر یافت نشد");

            OldImageName.Deleted = true;
            _PictureRepository.Update(OldImageName);
            _fileService.DeleteFile(Directories.ProductGalleryImage, OldImageName.ImageName);

            return OperationResult.Success();
        }

        public async Task<OperationResult> RemoveProductCategory(RemoveProductCategoryDto RemoveProductCategoryDto)
        {
            var product = await _repository.FindByIdAsync(RemoveProductCategoryDto.ProductID);
            if (product == null)
                return OperationResult.NotFound();

            var category = await _caegoryRepository.FindByIdAsync(RemoveProductCategoryDto.CategoryID);
            if (category == null)
                return OperationResult.NotFound();

            var productCategory = new ProductCategory()
            {
                CategoryID = RemoveProductCategoryDto.CategoryID,
                ProductID = RemoveProductCategoryDto.ProductID,
                UpdateON = DateTime.Now
            };

            productCategory.Deleted = true;
            _ProductCategoryRepository.Update(productCategory);
            return OperationResult.Success();
        }

        public async Task<OperationResult> RemoveProductPicture(RemoveProductPictureDto RemoveProductPicture)
        {
            var product = await _repository.FindByIdAsync(RemoveProductPicture.ProductID);
            if (product == null)
                return OperationResult.NotFound("!محصول مورد نظر یافت نشد");

            var picture = _PictureRepository.FindByIdAsync(RemoveProductPicture.PictureID);
            if (picture == null)
                return OperationResult.NotFound("!عکس مورد نظر یافت نشد");

            var productPicure = new ProductPicture()
            {
                PictureID = RemoveProductPicture.PictureID,
                ProductID = RemoveProductPicture.ProductID
            };

            productPicure.Deleted = true;
            _ProductPictureRepository.Update(productPicure);
            return OperationResult.Success();
        }

        public async Task<OperationResult> RemoveProductSpecification(RemoveProductSpecificationDto RemoveProductSpecification)
        {
            var Product = await _repository.FindByIdAsync(RemoveProductSpecification.ProductId);

            if (Product == null)
                return OperationResult.NotFound("!محصول مورد نظر یافت نشد");

            var productSpecification = await _ProductSpecificationRepository.FindByIdAsync(RemoveProductSpecification.ProductSpecificationId);

            if (productSpecification == null)
                return OperationResult.NotFound("!ویژگی مورد نظر یافت نشد");

            productSpecification.Deleted = true;
            _ProductSpecificationRepository.Update(productSpecification);
            return OperationResult.Success();
        }

        public async Task<OperationResult> UpdateProduct(EditProductDto EditProductDto)
        {
            var Product = await _repository.FindByIdAsync(EditProductDto.ProductId);
            if (Product == null)
                return OperationResult.NotFound();

            string oldImage = Product.ImageName;
            string NewImageName = Product.ImageName;

            if (EditProductDto.ImageFile != null)
            {
                NewImageName = await _fileService
                    .SaveFileAndGenerateName(EditProductDto.ImageFile, Directories.ProductImages);
            }

            Product.Description = EditProductDto.Description;
            Product.Name = EditProductDto.Title;
            Product.ImageName = NewImageName;
            Product.UpdateON = DateTime.Now;


            _repository.Update(Product);

            if (EditProductDto.ImageFile != null)
            {
                _fileService.DeleteFile(Directories.ProductImages, oldImage);
            }

            return OperationResult.Success();
        }


    }
}
