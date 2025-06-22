using Bookstore.Models;
using System.Collections.Generic;

namespace Bookstore.ViewModel
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public List<Book> RelatedBooks { get; set; }
        public bool IsInCart { get; set; }
    }
}
    