using Microsoft.AspNetCore.Mvc;
using shop.Frameworks;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.RoleCommand;
using shop.Service.Query;

namespace shop.Web.Controllers.Role
{
    public class RoleController : ShopController
    {
        private readonly IRoleService _roleService;
        private readonly RoleQueryService _roleQueryService;
        public RoleController(IRoleService roleService, RoleQueryService roleQueryService)
        {
            _roleService = roleService;
            _roleQueryService = roleQueryService;
        }

        [HttpPost("AddRole*")]
        public async Task<ApiResult> AddRole(CreateRoleDto Command)
        {
            var result = await _roleService.AddRole(Command);
            return CreatedResult(result,null);
        }

        [HttpPost("AddRolePermission*")]
        public async Task<ApiResult> AddRolePermission([FromForm]AddRolePermissionDto Command)
        {
            var result = await _roleService.AddRolePermission(Command);
            return CreatedResult(result, null);
        }

        [HttpPost("UpdateRole*")]
        public async Task<ApiResult> UpdateRole(EditRoleDto Command)
        {
            var result = await _roleService.UpdateRole(Command);
            return CommandResult(result);
        }

        [HttpDelete("RemoveRolePermission*")]
        public async Task<ApiResult> RemoveRolePermission(RemoveRolePermissionDto Command)
        {
            var result = await _roleService.RemoveRolePermission(Command);
            return CommandResult(result);
        }

        [HttpGet("GetRoleById*")]
        public async Task<ApiResult<RoleQueryDto?>> GetRoleById(int RoleId)
        {
            var result = await _roleQueryService.GetRoleById(RoleId);
            return QueryResult(result);
        }

        [HttpGet("GetAllRole*")]
        public async Task<ApiResult<List<RoleQueryDto?>>> GetAllRole()
        {
            var result = await _roleQueryService.GetAllRole();
            return QueryResult(result);
        }

    }
}
