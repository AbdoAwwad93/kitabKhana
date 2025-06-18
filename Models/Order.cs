using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    [PrimaryKey("Id")]
    public class Order
    {
        
        public string Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<Book> Books { get; set; }

    }
}
