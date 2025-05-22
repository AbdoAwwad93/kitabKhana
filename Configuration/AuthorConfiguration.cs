using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(author => author.Id);
            builder.Property(author => author.Name).HasColumnType("nVarChar(50)");
            builder.Property(author => author.About).HasColumnType("nvarchar(max)");
            builder.Property(author => author.ImageURL).HasColumnType("nvarchar(50)");
        }
    }
}
