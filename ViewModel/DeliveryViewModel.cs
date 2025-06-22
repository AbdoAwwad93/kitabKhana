using Bookstore.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.ViewModel
{
    public class DeliveryViewModel
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        [Display(Name = "الاسم الكامل")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Display(Name = "رقم الهاتف")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Required(ErrorMessage = "المدينة مطلوبة")]
        [Display(Name = "المدينة")]
        public string City { get; set; }

        [Required(ErrorMessage = "طريقة التوصيل مطلوبة")]
        [Display(Name = "طريقة التوصيل")]
        public DeliveryMethod DeliveryMethod { get; set; }

        public decimal Subtotal { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal Total { get; set; }
    }
} 