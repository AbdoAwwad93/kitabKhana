using Bookstore.Models;

namespace Bookstore.Repositories
{
    public interface IRepository<T> where T :class 
    {
        List<T> GetAll();
        T GetById(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
        void SaveChanges();
    }
}
