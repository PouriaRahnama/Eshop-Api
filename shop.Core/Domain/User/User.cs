using shop.Core.Commons;

namespace shop.Core.Domain.User
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarName { get; set; } = "avatar.png";

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<shop.Core.Domain.Order.Order> Orders { get; set; }
        public virtual ICollection<shop.Core.Domain.Comment.Comment> Comments { get; set; }
        public virtual ICollection<UserToken> UserToken { get; set; }
    }
}