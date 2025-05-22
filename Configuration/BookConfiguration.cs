using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);
            builder.Property(book => book.Title).HasColumnType("nVarchar(100)");
            builder.Property(book => book.ImageURL).HasColumnType("nVarchar(100)");
            builder.Property(book => book.Description).HasColumnType("nVarchar(MAX)");
            builder.Property(book => book.Type).HasColumnType("nVarchar(20)");
            
            builder.HasMany(book => book.Authors)
                .WithMany(author => author.Books)
                .UsingEntity<BookAuthor>();
            
            builder.HasMany(book => book.Comments)
                .WithOne(comment => comment.Book)
                .HasForeignKey(comment=>comment.BookId);

            builder.HasMany(book => book.Orders)
                .WithMany(order => order.Books)
                .UsingEntity<OrderItem>();
            

        }
    }
}
