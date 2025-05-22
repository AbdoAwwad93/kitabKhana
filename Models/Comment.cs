using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    [PrimaryKey("Id")]
    public class Comment
    {
        public int Id { get; set; }
        [Column(TypeName ="nVarChar(max)")]
        public string CommentText { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
