using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Username { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }
    }
}