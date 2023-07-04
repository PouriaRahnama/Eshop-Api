using IntelliTect.Coalesce.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Core.Domain.User;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.UserCommand;
using shop.Service.Query;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Web.ViewModels.Users;
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
    [HttpGet("UserFilter")]
    public async Task<ApiResult<UserFilterResult>> GetUserByFilter([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userQueryService.GetUserByFilter(filterParams);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpGet("{Id}")]
    public async Task<ApiResult<UserDto?>> GetById(int Id)
    {
        var result = await _userQueryService.GetUserById(Id);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpGet("UserByPhoneNumber")]
    public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string PhoneNumber)
    {
        var result = await _userQueryService.GetUserByPhoneNumber(PhoneNumber);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPost]
    public async Task<ApiResult> AddUser(CreateUserDto command)
    {
        var result = await _userService.AddUser(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPut]
    public async Task<ApiResult> EditUser([FromForm] EditUserDto command)
    {
        var result = await _userService.EditUser(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPost("UserRole")]
    public async Task<ApiResult> AddUserRole(AddUserRoleDto command)
    {
        var result = await _userService.AddUserRole(command);
        return CreatedResult(result, null);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpDelete("UserRole")]
    public async Task<ApiResult> RemoveUserRole(RemoveUserRoleDto command)
    {
        var result = await _userService.RemoveUserRole(command);
        return CommandResult(result);
    }

    [HttpPut("ChangePassword")]
    public async Task<ApiResult> ChangePassword(ChangePasswordViewModel ChangePasswordViewModel)
    {
        var UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var Command = new ChangePasswordDto()
        {
            Password = ChangePasswordViewModel.Password,
            CurrentPassword = ChangePasswordViewModel.CurrentPassword
        };

        Command.UserId = UserId;
        var result = await _userService.ChangePassword(Command);
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

