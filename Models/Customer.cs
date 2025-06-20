using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class Customer : IdentityUser
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime RegisterationDate { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
