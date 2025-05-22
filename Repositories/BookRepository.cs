
using Bookstore.DBContext;
using Bookstore.Models;

namespace Bookstore.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        AppDBContext context = new AppDBContext();

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
            //Book bookFromDb = context.Books.FirstOrDefault(x => x.Id==entity.Id)!;
            //bookFromDb.Title = entity.Title;
            //bookFromDb.Description = entity.Description;
            //bookFromDb.CategoryId = entity.CategoryId;
            //bookFromDb.ImageURL = entity.ImageURL;
            //bookFromDb.Type = entity.Type;
            context.Update(entity);
        }
    }
}
