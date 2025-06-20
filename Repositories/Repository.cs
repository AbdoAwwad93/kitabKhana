
using Bookstore.DBContext;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Repositories
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly AppDBContext context;
        private readonly DbSet<T> Dbset;

        public Repository(AppDBContext _Context)
        {
            context=_Context;
            Dbset = context.Set<T>();
        }

        public void Add(T entity)
        {
           context.Add(entity);
        }

        public void Delete(string id)
        {
            Dbset.Remove(GetById(id));
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Dbset;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }

        public T GetById(string id)
        {
            return Dbset.Find(id)!;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            Dbset.Update(entity);
        }
    }
}
