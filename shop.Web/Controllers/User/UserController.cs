using IntelliTect.Coalesce.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.UserCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;
using System.Security.Claims;

namespace shop.Web.Controllers.User;

[Authorize]

public class UserController : ShopController
{
    private readonly IUserService _userService;
    private readonly UserQueryService _userQueryService;

    public UserController(IUserService userService, UserQueryService userQueryService)
    {
        _userService = userService;
        _userQueryService = userQueryService;
    }

    [HttpGet("Current")]
    public async Task<ApiResult<UserDto>> GetCurrentUser()
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _userQueryService.GetUserById(userId);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpGet("GetUserByFilter")]
    public async Task<ApiResult<UserFilterResult>> GetUserByFilter([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userQueryService.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpGet("{userId}")]
    public async Task<ApiResult<UserDto?>> GetById(int userId)
    {
        var result = await _userQueryService.GetUserById(userId);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpGet("GetUserByPhoneNumber")]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string PhoneNumber)
    {
        var result = await _userQueryService.GetUserByPhoneNumber(PhoneNumber);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPost("AddUser")]
    public async Task<ApiResult> AddUser(CreateUserDto command)
    {
        var result = await _userService.AddUser(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPut("EditUser")]
    public async Task<ApiResult> EditUser([FromForm] EditUserDto command)
    {
        var result = await _userService.EditUser(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPost("AddUserRole")]
    public async Task<ApiResult> AddUserRole(AddUserRoleDto command)
    {
        var result = await _userService.AddUserRole(command);
        return CreatedResult(result, null);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpDelete("RemoveUserRole")]
    public async Task<ApiResult> RemoveUserRole(RemoveUserRoleDto command)
    {
        var result = await _userService.RemoveUserRole(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPost("ChangeWallet")]
    public async Task<ApiResult> ChangeWallet([FromForm] ChargeWalletDto command)
    {
        var result = await _userService.ChangeWallet(command);
        return CommandResult(result);
    }

}

