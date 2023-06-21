using shop.Core.Commons;

namespace shop.Core.Domain.Role
{
    public class Role:BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<RolePermission> Permissions { get; set; }
    }
}