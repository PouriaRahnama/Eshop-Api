using Microsoft.EntityFrameworkCore;
using shop.Core.Domain.User;
using shop.Data.ApplicationContext;
using shop.Service.Extension.Util;

namespace shop.Service.Query
{
    public class UserQueryService
    {
        private readonly IApplicationContext _context;
        public UserQueryService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
        {
            var @params = filterParams;
            var result = _context.Set<User>().OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.Email))
                result = result.Where(r => r.Email.Contains(@params.Email));

            if (!string.IsNullOrWhiteSpace(@params.PhoneNumber))
                result = result.Where(r => r.PhoneNumber.Contains(@params.PhoneNumber));

            if (@params.Id != null)
                result = result.Where(r => r.Id == @params.Id);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new UserFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(user => user.MapFilterData()).ToListAsync(),
                FilterParams = @params
            };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }

        public async Task<UserDto?> GetUserById(int userId)
        {
            var user = await _context.Set<User>()
                .FirstOrDefaultAsync(f => f.Id == userId);
            if (user == null)
                return null;

            var result = user.Map();

            return  result;
        }

        public async Task<UserDto?> GetUserByPhoneNumber(string PhoneNumber)
        {
            var user = await _context.Set<User>()
                .FirstOrDefaultAsync(f => f.PhoneNumber == PhoneNumber);

            if (user == null)
                return null;

            return user.Map();
        }

        public async Task<UserTokenDto> GetUserTokenByRefreshTokenQuery(string HashRefreshToken)
        {
            var UserToken =await _context.Set<UserToken>()
                .FirstOrDefaultAsync(ut => ut.HashRefreshToken == HashRefreshToken);

            var UserTokenDto = new UserTokenDto()
            {
                HashRefreshToken = UserToken.HashRefreshToken,
                HashJwtToken  = UserToken.HashJwtToken,
                RefreshTokenExpireDate = UserToken.RefreshTokenExpireDate,
                TokenExpireDate = UserToken.TokenExpireDate,
                Device = UserToken.Device,
                UserId = UserToken.UserId,
                Id = UserToken.Id
            };

            return UserTokenDto;
        }

    }
}
