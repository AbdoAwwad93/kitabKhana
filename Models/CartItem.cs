using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class CartItem
    {
        [Key]
        public string Id { get; set; }
        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public string BookId { get; set; }
        public virtual Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
} 