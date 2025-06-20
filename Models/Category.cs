using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public enum Category
    {
        [Display(Name = "كتب تاريخ")]
        History,
        [Display(Name = "كتب دينية")]
        Religious,
        [Display(Name = "روايات")]
        Literature,
        [Display(Name = "تنمية ذاتية وعلم نفس")]
        SelfDevelopment,
        [Display(Name = "كتب متنوعة")]
        Miscellaneous
    }
}
