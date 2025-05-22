using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    [PrimaryKey("BookId","OrderId")]
    public class OrderItem
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }

    }
}
