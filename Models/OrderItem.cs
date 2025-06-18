using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    [PrimaryKey("BookId","OrderId")]
    public class OrderItem
    {
        public string BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
        public string OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }

    }
}
