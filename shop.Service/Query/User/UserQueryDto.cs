using shop.Service.Extension.Util;
using shop.Service.Query.Commons;

namespace shop.Service.Query
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string AvatarName { get; set; }
        public List<UserRoleDto> Roles { get; set; }
    }

    public class AddressDto : BaseDto
    {
        public int UserId { get; set; }
        public string Shire { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PostalAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public bool ActiveAddress { get; set; }
    }

    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
    }

    public class UserTokenDto : BaseDto
    {
        public int UserId { get; set; }
        public string HashJwtToken { get; set; }
        public string HashRefreshToken { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public string Device { get; set; }
    }


    public class UserFilterData : BaseDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string AvatarName { get; set; }
    }

    public class UserFilterParams : BaseFilterParam
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? Id { get; set; }
    }
    public class UserFilterResult : BaseFilter<UserFilterData, UserFilterParams>
    {

    }
}
