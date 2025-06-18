using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Author
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string ImageURL { get; set; }
        public virtual List<Book> Books{ get; set; }
    }
}
