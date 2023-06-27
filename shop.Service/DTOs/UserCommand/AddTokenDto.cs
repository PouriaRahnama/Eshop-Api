namespace shop.Service.DTOs.UserCommand
{
    public class AddTokenDto
    {
        public int UserId { get; set; }
        public string HashJwtToken { get; set; }
        public string HashRefreshToken { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public string Device { get; set; }
    }
}
