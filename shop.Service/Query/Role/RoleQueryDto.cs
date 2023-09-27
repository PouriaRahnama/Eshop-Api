using shop.Core.Domain.Role;

namespace shop.Service.Query
{
    public class RoleQueryDto:BaseDto
    {
        public string Title { get; set; }
        public List<Permission> Permissions { get; set; }


    }
}
