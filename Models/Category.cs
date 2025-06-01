using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public enum Category
    {
        [Display (Name ="كتب تاريخ")]
        History,
        [Display(Name = "كتب دينية")]
        Religious,
        [Display(Name = "أدب")]
        Literature
    }
}
