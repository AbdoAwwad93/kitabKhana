using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Configuration
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(e => new { e.AuthorId, e.BookId });
            builder.Property(e=>e.AuthorId).ValueGeneratedNever();
            builder.Property(e=>e.BookId).ValueGeneratedNever();
            builder.HasData(BookAuthorData());
        }

        private List<BookAuthor> BookAuthorData()
        {
            return new List<BookAuthor>
            {
                new BookAuthor{AuthorId = "1", BookId ="10"},
                new BookAuthor{AuthorId = "1", BookId ="11"},
                new BookAuthor{AuthorId = "1", BookId ="12"},
                new BookAuthor{AuthorId = "1", BookId ="13"},
                new BookAuthor{AuthorId = "1", BookId ="14"},
                new BookAuthor{AuthorId = "2", BookId = "5"},
                new BookAuthor{AuthorId = "2", BookId = "7"},
                new BookAuthor{AuthorId = "3", BookId = "6"},
                new BookAuthor{AuthorId = "3", BookId = "8"},
                new BookAuthor{AuthorId = "3", BookId = "9"},
                new BookAuthor{AuthorId = "4", BookId = "1"},
                new BookAuthor{AuthorId = "4", BookId = "2"},
                new BookAuthor{AuthorId = "4", BookId = "3"},
                new BookAuthor{AuthorId = "5", BookId = "15"},
                new BookAuthor{AuthorId = "5", BookId = "16"},
                new BookAuthor{AuthorId = "5", BookId = "17"},
                new BookAuthor{AuthorId = "5", BookId = "18"},
                new BookAuthor{AuthorId = "5", BookId = "19"},
                new BookAuthor{AuthorId = "5", BookId = "20"},
                new BookAuthor{AuthorId = "8", BookId = "24"},
                new BookAuthor{AuthorId = "8", BookId = "25"},
                new BookAuthor{AuthorId = "6", BookId = "21"},
                new BookAuthor{AuthorId = "6", BookId = "22"},
                new BookAuthor{AuthorId = "7", BookId = "23"},
                new BookAuthor{AuthorId = "9", BookId = "26"},
                new BookAuthor{AuthorId = "10", BookId= "27"},
                new BookAuthor{AuthorId = "12", BookId= "28"},
                new BookAuthor{AuthorId = "13", BookId= "29"},
                new BookAuthor{AuthorId = "14" , BookId="30"},
                new BookAuthor{AuthorId = "14" , BookId="31"},
                new BookAuthor{AuthorId = "11" , BookId="32"},
                new BookAuthor{AuthorId = "4", BookId = "33"},
                new BookAuthor{AuthorId = "1", BookId = "34"},
                new BookAuthor{AuthorId = "1", BookId = "35"},
                new BookAuthor{AuthorId = "3", BookId = "36"},
                new BookAuthor{AuthorId = "15" , BookId ="4"},

            };
        }
    }
}
