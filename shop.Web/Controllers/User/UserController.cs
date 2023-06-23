using Microsoft.AspNetCore.Mvc;
using shop.Frameworks;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.UserCommand;
using shop.Service.Query;

namespace shop.Web.Controllers.User
{
    public class UserController : ShopController
    {
        private readonly IUserService _userService;
        private readonly UserQueryService _userQueryService;

        public UserController(IUserService userService, UserQueryService userQueryService)
        {
            _userService = userService;
            _userQueryService = userQueryService;
        }

        [HttpGet("GetUserByFilter")]
        public async Task<ApiResult<UserFilterResult>> GetUserByFilter([FromQuery]UserFilterParams filterParams)
        {
            var result = await _userQueryService.GetUserByFilter(filterParams);
            return QueryResult(result);
        }

        [HttpGet("{userId}")]
        public async Task<ApiResult<UserDto?>> GetById(int userId)
        {
            var result = await _userQueryService.GetUserById(userId);
            return QueryResult(result);
        }

        [HttpGet("GetUserByPhoneNumber")]
        public async Task<ApiResult<UserDto?>> GetUserByPhoneNumber(string PhoneNumber)
        {
            var result = await _userQueryService.GetUserByPhoneNumber(PhoneNumber);
            return QueryResult(result);
        }

        [HttpPost("AddUser")]
        public async Task<ApiResult> AddUser(CreateUserDto command)
        {
            var result = await _userService.AddUser(command);
            return CommandResult(result);
        }

        [HttpPut("EditUser")]
        public async Task<ApiResult> EditUser([FromForm]EditUserDto command)
        {
            var result = await _userService.EditUser(command);
            return CommandResult(result);
        }

        [HttpPost("AddUserRole")]
        public async Task<ApiResult> AddUserRole(AddUserRoleDto command)
        {
            var result = await _userService.AddUserRole(command);
            return CreatedResult(result,null);
        }

        [HttpDelete("RemoveUserRole")]
        public async Task<ApiResult> RemoveUserRole(RemoveUserRoleDto command)
        {
            var result = await _userService.RemoveUserRole(command);
            return CommandResult(result);
        }

       
        [HttpPost("ChangeWallet")]
        public async Task<ApiResult> ChangeWallet([FromForm]ChargeWalletDto command)
        {
            var result = await _userService.ChangeWallet(command);
            return CommandResult(result);
        }

    }
}
