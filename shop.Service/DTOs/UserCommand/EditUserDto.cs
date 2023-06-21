using Microsoft.AspNetCore.Http;

namespace shop.Service.DTOs.UserCommand
{
    public class EditUserDto
    {
        public long UserId { get; set; }
        public IFormFile? Avatar { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
