using System.Reflection;

namespace shop.Service.DTOs.UserCommand
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
