using shop.Core.Commons;

namespace shop.Core.Domain.User
{
    public class UserRole: RelationBaseEntity
    {
        public int UserId { get;  set; }
        public int RoleId { get;  set; }
        public virtual User User { get; set; }
        public virtual shop.Core.Domain.Role.Role Role { get;  set; }

    }
}