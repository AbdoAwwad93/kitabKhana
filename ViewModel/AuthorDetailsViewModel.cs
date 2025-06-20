using Bookstore.Models;

namespace Bookstore.ViewModel
{
    public class AuthorDetailsViewModel
    {
        public  Author Author { get; set; }
        public  List<Author> RelatedAuthors { get; set; }
    }
}
