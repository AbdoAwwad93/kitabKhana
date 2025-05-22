using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DBContext
{
    public class AppDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                .Build();
            var contstr = configuration.GetSection("constr").Value;
            optionsBuilder.UseSqlServer(contstr);
        }
    }
}
