namespace shop.Service.DTOs.UserCommand
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
    }
}
