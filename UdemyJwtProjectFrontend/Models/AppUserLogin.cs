using System.ComponentModel.DataAnnotations;

namespace UdemyJwtProjectFrontend.Models
{
    public class AppUserLogin
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola gereklidir")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }
    }
}