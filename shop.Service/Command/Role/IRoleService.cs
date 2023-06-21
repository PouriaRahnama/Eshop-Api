using shop.Service.DTOs.RoleCommand;
using shop.Service.Extension.Util;

namespace shop.Service.Command
{
    public interface IRoleService
    {
        Task<OperationResult> AddRole(CreateRoleDto CreateRoleDto);
        Task<OperationResult> AddRolePermission(AddRolePermissionDto AddRolePermissionDto);
        Task<OperationResult> UpdateRole(EditRoleDto EditRoleDto);
        Task<OperationResult> RemoveRolePermission(RemoveRolePermissionDto RemoveRolePermissionDto);

    }
}
