using shop.Core.Commons;

namespace shop.Core.Domain.User;

public class UserToken : BaseEntity
{
    public int UserId { get; set; }
    public string HashJwtToken { get; set; }
    public string HashRefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Device { get; set; }

    public virtual User User { get; set; }

}