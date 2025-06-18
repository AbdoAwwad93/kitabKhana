using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class BookAuthor
    {
        public string BookId { get; set; }
        public string AuthorId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
