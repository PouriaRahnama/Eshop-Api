using shop.Core.Domain.User;

namespace shop.Service.DTOs.UserCommand
{
    public class ChargeWalletDto
    {
        public int UserId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsFinally { get; set; }
        public WalletType Type { get; set; }
    }
}
