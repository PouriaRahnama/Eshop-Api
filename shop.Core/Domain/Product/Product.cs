using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        public virtual ICollection<shop.Core.Domain.Comment.Comment> Comments { get; set; }
        public virtual ICollection<ProductSpecification> Specifications { get; set; }
    }
}