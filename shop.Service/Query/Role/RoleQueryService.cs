using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.Role;
using shop.Data.ApplicationContext;

namespace shop.Service.Query
{
    public class RoleQueryService
    {
        private readonly IApplicationContext _Context;

        public RoleQueryService(IApplicationContext Context)
        {
            _Context = Context;
        }

        public async Task<RoleQueryDto?> GetRoleById(int RoleId)
        {
            var role = await _Context.Set<Role>().FirstOrDefaultAsync(f => f.Id == RoleId);
            if (role == null)
                return null;

            return new RoleQueryDto()
            {
                Id = role.Id,
                CreationDate = role.CreateON,
                Permissions = role.Permissions.Where(p => p.Deleted == false).Select(s => s.PermissionStatus).ToList(),
                Title = role.Title
            };
        }

        public async Task<List<RoleQueryDto>> GetAllRole()
        {
            return await _Context.Set<Role>().Select(role => new RoleQueryDto()
            {
                Id = role.Id,
                CreationDate = role.CreateON,
                Permissions = role.Permissions.Where(p => p.Deleted == false).Select(s => s.PermissionStatus).ToList(),
                Title = role.Title
            }).ToListAsync();
        }


    }
}
