using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Shop.Api.ViewModels.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "!شماره تلفن را وارد کنید")]
    [MaxLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "!کلمه عبور را وارد کنید")]
    [MinLength(6,ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    public string Password { get; set; }


    [Required(ErrorMessage = "!تکرار کلمه عبور را وارد کنید")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
    [Compare(nameof(Password),ErrorMessage = "کلمه های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; }


    [Required(ErrorMessage = "!ایمیل را وارد کنید")]
    [EmailAddress(ErrorMessage = "!ایمیل را به درستی وارد کنید")]
    public string Email { get; set; }


    [Required(ErrorMessage = "!لطفا نام خانوادگی را وارد کنید")]
    public string Family { get; set; }


    [Required(ErrorMessage = "!نام را وارد کنید")]
    public string Name { get; set; }
}