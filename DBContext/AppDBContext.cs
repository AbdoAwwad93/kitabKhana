using Bookstore.Configuration;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bookstore.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions <AppDBContext> options)
        :base(options)
        {
            
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
        }
    }
}
