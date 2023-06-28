using shop.Core.Domain.User;

namespace shop.Service.Query
{
    public static class UserQueryMapper
    {
        public static UserDto Map(this User user)
        {
            return new UserDto()
            {
                IsActive = user.IsActive,
                Id = user.Id,
                CreationDate = user.CreateON,
                Family = user.Family,
                PhoneNumber = user.PhoneNumber,
                AvatarName = user.AvatarName,
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Roles = user.Roles.Select(s => new UserRoleDto()
                {
                    RoleId = s.RoleId,
                    RoleTitle = s.Role.Title
                }).ToList()
            };
        }

        public static UserFilterData MapFilterData(this User user)
        {
            return new UserFilterData()
            {
                Id = user.Id,
                CreationDate = user.CreateON,
                Family = user.Family,
                PhoneNumber = user.PhoneNumber,
                AvatarName = user.AvatarName,
                Email = user.Email,
                Name = user.Name
            };
        }



    }
}
