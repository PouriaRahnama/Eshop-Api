using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Core.Domain.Product;

namespace shop.Data.Mapping
{
    public class PictureMap : IEntityTypeConfiguration<Picture>
    {

        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
