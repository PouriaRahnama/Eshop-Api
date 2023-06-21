using shop.Core.Domain.Role;
using System.Security;

namespace shop.Service.DTOs.RoleCommand
{
    public class AddRolePermissionDto
    {
        public int RoleId { get; set; }
        public Permission PermissionStatus { get; set; }
    }
}
