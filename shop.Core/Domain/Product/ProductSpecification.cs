using shop.Core.Commons;

namespace shop.Core.Domain.Product
{
    public class ProductSpecification:BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Product Product { get; set; }
    }
}