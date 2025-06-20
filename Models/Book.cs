namespace Bookstore.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public Category Category { get; set; }
        public int Price { get; set; }
        public virtual List<Author> Authors { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Cart> Carts { get; set; }

    }
}