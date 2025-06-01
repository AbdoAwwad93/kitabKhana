using Bookstore.Models;

namespace Bookstore.Repositories
{
    public interface IRepository
    {
        List<Book> GetAll();
        Book GetById(int id);
        void Add(Book entity);
        void Update(Book entity);
        void Delete(int id);
        void SaveChanges();
    }
}
