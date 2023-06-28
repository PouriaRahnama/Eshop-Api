using Microsoft.AspNetCore.Authentication.JwtBearer;
using shop.Service.Extension.SecurityUtil;
using shop.Service.Query;
using System.Security.Claims;

namespace Shop.Api.Infrastructure.JwtUtil;

public class CustomJwtValidation
{
    private readonly UserQueryService _userQueryService;

    public CustomJwtValidation(UserQueryService userQueryService)
    {
        _userQueryService = userQueryService;
    }

    public async Task Validate(TokenValidatedContext context)
    {

        var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var hashJwtToken = Sha256Hasher.Hash(jwtToken);
        var token = await _userQueryService.GetUserTokenByJwtTokenQuery(hashJwtToken);
        if (token == null)
        {
            context.Fail("Token NotFound");
            return;
        }

        var user = await _userQueryService.GetUserById(Convert.ToInt32(userId));
        if (user == null || user.IsActive == false )
        {
            context.Fail("User InActive");
            return;
        }
    }
}