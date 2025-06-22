using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.ViewModel
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="يجب إدخال الاسم")]
        [Display(Name ="الأسم بالكامل")]
        [RegularExpression(@"^[أ-ي\s]+$", ErrorMessage = "الاسم يجب أن يكون باللغة العربية")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "يجب إدخال البريد الشخصي")]
        [Display(Name ="البريد الشخصي")]
        public string Email { get; set; }
        [Required(ErrorMessage = "يجب إدخال اسم المستخدم")]
        [Display(Name ="(Username)اسم المستخدم")]
        [Remote("CheckUserNameAvailability", "Account", ErrorMessage = "اسم المستخدم مستخدم بالفعل")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "يجب إدخال رقم الهاتف")]
        [Display(Name ="رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "يجب إدخال العنوان")]
        [Display(Name ="العنوان")]
        public string Address { get; set; }
        [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
        [Display(Name ="كلمة المرور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "يجب إدخال كلمة المرور مرة أخرى")]
        [Display(Name ="تأكيد كلمة المرور")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "كلمة المرور وتأكيدها غير متطابقتين")]
        public string ConfirmPassword { get; set; }
    }
}
