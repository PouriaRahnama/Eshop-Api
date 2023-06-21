using shop.Core.Commons;

namespace shop.Core.Domain.Product
{
    public class ProductPicture : RelationBaseEntity
    {
        public int ProductID { get; set; }
        public int PictureID { get; set; }


        public virtual Product Product { get; set; }
        public virtual Picture Picture { get; set; }
    }
}