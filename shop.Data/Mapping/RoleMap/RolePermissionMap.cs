using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Role;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shop.Core.Domain.Comment;

namespace shop.Data.Mapping.RoleMap
{
    public class RolePermissionMap : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {

            //builder.Ignore(r => r.PermissionStatus);

            builder.Property(d => d.PermissionStatus)
                 .HasConversion(new EnumToStringConverter<Permission>());
        }
    }
}
