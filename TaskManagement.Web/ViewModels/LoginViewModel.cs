using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Web.ViewModels;

public class LoginViewModel
{
    [Display(Name = "ایمیل")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "رمز عبور")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "مرا به خاطر بسپار")]
    public bool RememberMe { get; set; }
}
