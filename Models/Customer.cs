using Microsoft.AspNetCore.Identity;

namespace Bookstore.Models
{
    public class Customer:IdentityUser
    {
        public string Address { get; set; }
        public DateTime RegisterationDate { get; set; }
    }
}
