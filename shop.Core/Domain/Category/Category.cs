using shop.Core.Commons;
using shop.Core.Domain.Product;

namespace shop.Core.Domain.Category
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentID { get; set; }

        
        public virtual Category PatentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<ProductCategory> productCategories { get; set; }

    }
}