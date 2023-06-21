using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Category;
using shop.Core.Domain.User;

namespace shop.Data.Mapping.UserMap
{
    internal class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(p => new { p.UserId, p.RoleId });
        }
    }
}
