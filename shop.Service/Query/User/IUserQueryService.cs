namespace shop.Service.Query
{
    public interface IUserQueryService
    {
        Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);

        Task<UserDto?> GetUserById(int userId);

        Task<UserDto?> GetUserByPhoneNumber(string PhoneNumber);

        Task<UserTokenDto> GetUserTokenByRefreshTokenQuery(string HashRefreshToken);
  
        Task<UserTokenDto> GetUserTokenByJwtTokenQuery(string HashJwtToken);

        Task<AddressDto?> GetUserAddressById(int AddressId);

        Task<List<AddressDto?>> GetUserAddress(int UserId);
    }
}
