using shop.Core.Commons;

namespace shop.Core.Domain.Product
{
    public class ProductCategory : RelationBaseEntity
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public virtual Product Product { get; set; }
        public virtual shop.Core.Domain.Category.Category Category { get; set; }
    }
}