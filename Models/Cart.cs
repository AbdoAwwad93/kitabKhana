using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<Book> Books { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
