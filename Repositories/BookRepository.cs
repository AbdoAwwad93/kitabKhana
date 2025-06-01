
using Bookstore.DBContext;
using Bookstore.Models;

namespace Bookstore.Repositories
{
    public class BookRepository : IRepository
    {
        private readonly AppDBContext context;

        public BookRepository(AppDBContext _Context)
        {
            context=_Context;
        }

        public void Add(Book entity)
        {
           context.Add(entity);
        }

        public void Delete(int id)
        {
            var book = context.Books.FirstOrDefault(x => x.Id==id);
            context.Books.Remove(book!);
        }

        public List<Book> GetAll()
        {
            return context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return context.Books.FirstOrDefault(book => book.Id==id)!;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Book entity)
        {
            context.Update(entity);
        }
    }
}
