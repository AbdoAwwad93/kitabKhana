using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 