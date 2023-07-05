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

        public static AddressDto MapUserAddress(this UserAddress address)
        {
            return new AddressDto()
            {
                Name = address.Name,
                Id = address.Id,
                ActiveAddress = address.ActiveAddress,
                City = address.City,
                CreationDate = address.CreateON,
                Family = address.Family,
                PhoneNumber = address.PhoneNumber,
                NationalCode = address.NationalCode,
                PostalAddress = address.PostalAddress,
                PostalCode = address.PostalCode,
                Shire = address.Shire,
                UserId = address.UserId
            };
        }

        public static List<AddressDto> MapUserAddress(this List<UserAddress> addresses)
        {
            var AddressDto = new List<AddressDto>();
            foreach (var address in addresses)
            {
                AddressDto.Add(new AddressDto()
                {
                    Name = address.Name,
                    Id = address.Id,
                    ActiveAddress = address.ActiveAddress,
                    City = address.City,
                    CreationDate = address.CreateON,
                    Family = address.Family,
                    PhoneNumber = address.PhoneNumber,
                    NationalCode = address.NationalCode,
                    PostalAddress = address.PostalAddress,
                    PostalCode = address.PostalCode,
                    Shire = address.Shire,
                    UserId = address.UserId
                });
            }

            return AddressDto;

        }



    }
}
