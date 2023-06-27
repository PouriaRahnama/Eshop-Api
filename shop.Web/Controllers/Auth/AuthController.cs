using AngleSharp.Browser;
using Microsoft.AspNetCore.Mvc;
using shop.Frameworks.Commons;
using shop.Service.Command;
using shop.Service.DTOs.UserCommand;
using shop.Service.Extension.SecurityUtil;
using shop.Service.Extension.Util;
using shop.Service.Query;
using Shop.Api.ViewModels.Auth;
using Shop.Web.Infrastructure.JwtUtil;
using UAParser;

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
    public async Task<ApiResult<LoginResultDto?>> Login(LoginViewModel loginViewModel)
    { 

        var user = await _userQueryService.GetUserByPhoneNumber(loginViewModel.PhoneNumber);
        if (user == null)
        {
            var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
            return CommandResult(result);
        }

        if (Sha256Hasher.IsCompare(user.Password, loginViewModel.Password) == false)
        {
            var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
            return CommandResult(result);
        }

        var loginResult = await AddTokenAndGenerateJwt(user);
        return CommandResult(loginResult);
    }



    [HttpPost("register")]
    public async Task<ApiResult> Register(RegisterViewModel register)
    {
        
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



    private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
    {
        var uaParser = Parser.GetDefault();
        var info = uaParser.Parse(HttpContext.Request.Headers["user-agent"]);
        var device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var hashJwt = Sha256Hasher.Hash(token);
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);

        var addtoken = new AddTokenDto()
        {
            UserId = user.Id,
            Device = device,
            HashJwtToken = hashJwt,
            HashRefreshToken = hashRefreshToken,
            RefreshTokenExpireDate = DateTime.Now.AddDays(8),
            TokenExpireDate = DateTime.Now.AddDays(7),

        };
        var tokenResult = await _userService.AddToken(addtoken);

        if (tokenResult.Status != OperationResultStatus.Success)
            return OperationResult<LoginResultDto?>.Error(tokenResult.Message);

        return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
        {
            Token = token,
            RefreshToken = refreshToken
        });
    }

}