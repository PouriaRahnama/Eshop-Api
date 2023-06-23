using shop.Core.Commons;
using shop.Core.Domain.Comment;

namespace shop.Core.Domain.User
{
    public class Wallet:BaseEntity
    {

        public int UserId { get; set; }
        public int Price { get; set; }
        public string Desciption { get; set; }
        public int StatusId { get; set; }
        public WalletType Status
        {
            get => (WalletType)StatusId;
            set => StatusId = (int)value;
        }

        public bool IsFinally { get; set; }
        public DateTime? FinallyDate { get; set; }
        public virtual User User { get; set; }

    }
    public enum WalletType
    {
        Deposit = 0,
        Withdrawal = 1
    }
}