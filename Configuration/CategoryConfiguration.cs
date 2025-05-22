using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category => category.Id);
            builder.Property(category => category.Name).HasColumnType("Nvarchar(20)");
            builder.Property(category => category.Description).HasColumnType("Nvarchar(Macategory)");
            builder.HasMany(category=>category.Books)
                .WithOne(book=>book.Category)
                .HasForeignKey(book=>book.CategoryId);
        }
    }
}
