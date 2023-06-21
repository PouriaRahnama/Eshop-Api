using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Role
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public Permission PermissionStatus
        {
            get => (Permission)PermissionId;
            set => PermissionId = (int)value;
        }
        public virtual Role Role { get; set; }
    }
    public enum Permission
    {
        AdminPanel = 10 ,
        UserPanel = 20,
        EditProfile = 30 ,
        ChangePassword = 40
    }
}
