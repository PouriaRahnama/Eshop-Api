using shop.Core.Commons;

namespace shop.Core.Domain.Product
{
    public class Picture : BaseEntity
    {
        public string ImageName { get;  set; }

        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
    }
}