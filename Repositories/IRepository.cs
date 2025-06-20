using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Repositories
{
    public interface IRepository<T> where T :class 
    {
        List<T> GetAll(params Expression<Func<T, object>>[] includes);
        T GetById(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
        void SaveChanges();
    }
}
