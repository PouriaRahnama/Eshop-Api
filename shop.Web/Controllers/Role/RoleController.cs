using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.RoleCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;

namespace shop.Web.Controllers.Role;

[PermissionChecker(Permission.Role_Management)]
public class RoleController : ShopController
{
    private readonly IRoleService _roleService;
    private readonly IRoleQueryService _roleQueryService;
    public RoleController(IRoleService roleService, IRoleQueryService roleQueryService)
    {
        _roleService = roleService;
        _roleQueryService = roleQueryService;
    }

    [HttpPost]
    public async Task<ApiResult> AddRole(CreateRoleDto Command)
    {
        var result = await _roleService.AddRole(Command);
        return CreatedResult(result, null);
    }

    [HttpPost("RolePermission")]
    public async Task<ApiResult> AddRolePermission([FromForm] AddRolePermissionDto Command)
    {
        var result = await _roleService.AddRolePermission(Command);
        return CreatedResult(result, null);
    }

    [HttpPut]
    public async Task<ApiResult> UpdateRole(EditRoleDto Command)
    {
        var result = await _roleService.UpdateRole(Command);
        return CommandResult(result);
    }

    [HttpDelete("RolePermission")]
    public async Task<ApiResult> RemoveRolePermission(RemoveRolePermissionDto Command)
    {
        var result = await _roleService.RemoveRolePermission(Command);
        return CommandResult(result);
    }

    [HttpGet("{Id}")]
    public async Task<ApiResult<RoleQueryDto?>> GetRoleById(int Id)
    {
        var result = await _roleQueryService.GetRoleById(Id);
        return QueryResult(result);
    }

    [HttpGet("AllRole")]
    public async Task<ApiResult<List<RoleQueryDto?>>> GetAllRole()
    {
        var result = await _roleQueryService.GetAllRole();
        return QueryResult(result);
    }
}

