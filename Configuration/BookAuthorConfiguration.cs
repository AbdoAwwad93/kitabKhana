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
            builder.HasData(BookAuthorData());
        }

        private List<BookAuthor> BookAuthorData()
        {
            return new List<BookAuthor>
            {
                new BookAuthor{AuthorId = 1, BookId = 10},
                new BookAuthor{AuthorId = 1, BookId = 11},
                new BookAuthor{AuthorId = 1, BookId = 12},
                new BookAuthor{AuthorId = 1, BookId = 13},
                new BookAuthor{AuthorId = 1, BookId = 14},
                new BookAuthor{AuthorId = 2, BookId = 5},
                new BookAuthor{AuthorId = 2, BookId = 7},
                new BookAuthor{AuthorId = 3, BookId = 6},
                new BookAuthor{AuthorId = 3, BookId = 8},
                new BookAuthor{AuthorId = 3, BookId = 9},
                new BookAuthor{AuthorId = 4, BookId = 1},
                new BookAuthor{AuthorId = 4, BookId = 2},
                new BookAuthor{AuthorId = 4, BookId = 3},
            };
        }
    }
}
