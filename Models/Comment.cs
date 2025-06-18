using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    [PrimaryKey("Id")]
    public class Comment
    {
        public string Id { get; set; }
        [Column(TypeName ="nVarChar(max)")]
        public string CommentText { get; set; }
        public int Rate { get; set; }

        public virtual Book Book { get; set; }
        public string BookId { get; set; }
    }
}
