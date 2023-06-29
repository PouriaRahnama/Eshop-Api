using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using shop.Core.Domain.Role;
using shop.Service.Command;
using shop.Service.Query;
using System.Security.Claims;

namespace Shop.Api.Infrastructure.JwtUtil;

public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private IUserService _userService;
    private IRoleService _roleService;
    private RoleQueryService _roleQueryService;
    private UserQueryService _userQueryService;
    private readonly Permission _permission;

    public PermissionChecker(Permission permission)
    {
        _permission = permission;
    }



    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context))
            return;

        _userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        _roleService = context.HttpContext.RequestServices.GetRequiredService<IRoleService>();
        _roleQueryService = context.HttpContext.RequestServices.GetRequiredService<RoleQueryService>();
        _userQueryService = context.HttpContext.RequestServices.GetRequiredService<UserQueryService>();
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            if (await UserHasPermission(context) == false)
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("Unauthorize");
        }
    }




    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
        bool hasAllowAnonymous = false;
        foreach (var f in metaData)
        {
            try
            {
                hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                if (hasAllowAnonymous)
                    break;
            }
            catch
            {
                // ignored
            }
        }
        return hasAllowAnonymous;
    }



    private async Task<bool> UserHasPermission(AuthorizationFilterContext context)
    {
        var id =  Convert.ToInt32(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userQueryService.GetUserById(id);
        if (user == null)
            return false;

        var roleIds = user.Roles.Select(s => s.RoleId).ToList();       
        var roles = await _roleQueryService.GetAllRole();
        var userRoles = roles.Where(r => roleIds.Contains(r.Id));

        return userRoles.Any(r => r.Permissions.Contains(_permission));
    }




}