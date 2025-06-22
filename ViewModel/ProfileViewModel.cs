using System.ComponentModel.DataAnnotations;
using Bookstore.Models;
using System.Collections.Generic;

namespace Bookstore.ViewModel
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "يجب إدخال الاسم")]
        [Display(Name = "الأسم بالكامل")]
        [RegularExpression(@"^[أ-ي\s]+$", ErrorMessage = "الاسم يجب أن يكون باللغة العربية")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "يجب إدخال البريد الشخصي")]
        [Display(Name = "البريد الشخصي")]
        public string Email { get; set; }
        [Required(ErrorMessage = "يجب إدخال اسم المستخدم")]
        [Display(Name = "(Username)اسم المستخدم")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "يجب إدخال رقم الهاتف")]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "يجب إدخال العنوان")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        public List<Book> PurchasedBooks { get; set; }
    }
}
