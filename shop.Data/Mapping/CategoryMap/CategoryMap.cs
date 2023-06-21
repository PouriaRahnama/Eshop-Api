using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Core.Domain.Category;

namespace shop.Data.Mapping.CategoryMap
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(p => p.ChildCategories)
                .WithOne(p => p.PatentCategory)
                .HasForeignKey(p => p.ParentID)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            builder.ToTable("Categories", "Category");

            builder.Property(b => b.Name)
                .HasMaxLength(350)
                .IsRequired();

            builder.HasKey(x => x.Id);

        }
    }
}
