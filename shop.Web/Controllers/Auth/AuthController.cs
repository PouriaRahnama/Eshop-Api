using Microsoft.AspNetCore.Mvc;
using shop.Frameworks;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.UserCommand;
using shop.Service.Extension.SecurityUtil;
using shop.Service.Extension.Util;
using shop.Service.Query;
using Shop.Api.ViewModels.Auth;
using Shop.Web.Infrastructure.JwtUtil;

namespace shop.Web.Controllers.Auth;

public class AuthController : ShopController
{
    private readonly IUserService _userService;
    private readonly UserQueryService _userQueryService;
    private readonly IConfiguration _configuration;
    public AuthController(IUserService userService, IConfiguration configuration, UserQueryService userQueryService)
    {
        _userService = userService;
        _configuration = configuration;
        _userQueryService = userQueryService;
    }

    [HttpPost("login")]
    public async Task<ApiResult<string?>> Login(LoginViewModel loginViewModel)
    { 
        if (ModelState.IsValid == false)
        {
            return new ApiResult<string?>()
            {
                Data = null,
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors()
                }
            };
        }
        var user = await _userQueryService.GetUserByPhoneNumber(loginViewModel.PhoneNumber);
        if (user == null)
        {
            var result = OperationResult<string>.Error("کاربری با مشخصات وارد شده یافت نشد");
            return CommandResult(result);
        }

        if (Sha256Hasher.IsCompare(user.Password, loginViewModel.Password) == false)
        {
            var result = OperationResult<string>.Error("کاربری با مشخصات وارد شده یافت نشد");
            return CommandResult(result);
        }
        //
        //if (user.IsActive == false)
        //{
        //    var result = OperationResult<string>.Error("حساب کاربری شما غیرفعال است");
        //    return CommandResult(result);
        //}

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        return new ApiResult<string?>()
        {
            Data = token,
            IsSuccess = true,
            MetaData = new()
        };
    }

    [HttpPost("register")]
    public async Task<ApiResult> Register(RegisterViewModel register)
    {
        if (ModelState.IsValid == false)
        {
            return new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = JoinErrors()
                }
            };
        }
        
        var command = new CreateUserDto()
        {
            PhoneNumber = register.PhoneNumber,
            Password = register.Password,
            Email = register.Email,
            Family = register.Family,
            Name = register.Name
        };
        var result = await _userService.AddUser(command);
        return CommandResult(result);
    }
}