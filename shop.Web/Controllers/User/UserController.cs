using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shop.Core.Domain.Role;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.UserCommand;
using shop.Service.Query;
using shop.Web.Infrastructure;
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
        var userId = User.GetUserId();
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


    [HttpPut("Current")]
    public async Task<ApiResult> EditUser([FromForm] EditUserViewModel command)
    {
        var UserDto = new EditUserDto()
        {
            Avatar = command.Avatar,
            Email = command.Email,
            Family = command.Family,
            Name = command.Name,
            PhoneNumber = command.PhoneNumber
        };
        UserDto.UserId = User.GetUserId();
        var result = await _userService.EditUser(UserDto);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.User_Management)]
    [HttpPut]
    public async Task<ApiResult> Edit([FromForm] EditUserDto command)
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
        var UserId = User.GetUserId();
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


    [HttpPost("UserAddress")]
    public async Task<ApiResult> AddUserAddress(AddUserAddressViewModel command)
    {
        var userAddressDto = new AddUserAddressDto()
        {
            City = command.City,
            Family = command.Family,
            Name = command.Name,
            NationalCode = command.NationalCode,
            PhoneNumber = command.PhoneNumber,
            PostalAddress = command.PostalAddress,
            PostalCode = command.PostalCode,
            Shire = command.Shire
        };
        var userId = User.GetUserId();
        userAddressDto.UserId = userId;
        var result = await _userService.AddUserAddress(userAddressDto);
        return CommandResult(result);
    }

    [HttpPut("UserAddress")]
    public async Task<ApiResult> EditUserAddress(EditUserAddressViewModel command)
    {
        var userAddressDto = new EditUserAddressDto()
        {
            City = command.City,
            Family = command.Family,
            Name = command.Name,
            NationalCode = command.NationalCode,
            PhoneNumber = command.PhoneNumber,
            PostalAddress = command.PostalAddress,
            PostalCode = command.PostalCode,
            Shire = command.Shire,
            Id = command.Id
        };
        var userId = User.GetUserId();
        userAddressDto.UserId = userId;
        var result = await _userService.EditUserAddress(userAddressDto);
        return CommandResult(result);
    }

    [HttpGet("Address/{id}")]
    public async Task<ApiResult<AddressDto?>> GetUserAddressById(int id)
    {
        var result = await _userQueryService.GetUserAddressById(id);
        return QueryResult(result);
    }

    [HttpGet("Address")]
    public async Task<ApiResult<List<AddressDto?>>> GetUserAddress()
    {
        var userId = User.GetUserId();
        var result = await _userQueryService.GetUserAddress(userId);
        return QueryResult(result);
    }

    [HttpDelete("{addressId}")]
    public async Task<ApiResult> Delete(int addressId)
    {
        var result = await _userService.RemoveUserAddress(new RemoveUserAddressDto()
        {
            AddressId = addressId,
            UserId = User.GetUserId()
        });
        return CommandResult(result);
    }

    [HttpPut("SetActiveAddress/{addressId}")]
    public async Task<ApiResult> SetAddressActive(int addressId)
    {
        var command = new SetActiveUserAddressDto()
        {
            AddressId = addressId,
            UserId = User.GetUserId()
        };

        var result = await _userService.SetActiveUserAddress(command);
        return CommandResult(result);
    }

}


