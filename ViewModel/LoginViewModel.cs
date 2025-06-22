using System.ComponentModel.DataAnnotations;

namespace Bookstore.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="يجب إدخال البريد الإلكتروني")]
        [Display(Name ="البريد الإلكتروني")]
        public string Email { get; set; }
        [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
        [Display(Name = "كلمة المرور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تذكرني")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
