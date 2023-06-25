using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Role
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public Permission PermissionStatus { get; set; }
        //public int PermissionId { get; set; }
        //public Permission PermissionStatus
        //{
        //    get => (Permission)PermissionId;
        //    set => PermissionId = (int)value;
        //}
        public virtual Role Role { get; set; }
    }
    public enum Permission
    {
        //0
        AdminPanel,
        //1
        UserPanel,
        //2
        EditProfile,
        //3
        ChangePassword
    }
}
