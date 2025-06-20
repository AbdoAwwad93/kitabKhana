using Bookstore.Models;
using Bookstore.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IRepository<Author> repo;
        private readonly IRepository<Customer> customerRepo;

        public AuthorController(IRepository<Author> _repo, IRepository<Customer> customerRepo)
        {
            repo = _repo;
            this.customerRepo = customerRepo;
        }
        public IActionResult Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            int cartCount = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = customerRepo.GetById(userId);
                cartCount = user?.Cart?.Books?.Count ?? 0;
            }
            ViewBag.CartCount = cartCount;
            var authors = repo.GetAll();
            return View(authors);
        }
        public IActionResult Details(string id)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            int cartCount = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = customerRepo.GetById(userId);
                cartCount = user?.Cart?.Books?.Count ?? 0;
            }
            ViewBag.CartCount = cartCount;
            var author = repo.GetById(id);
            var relatedAuthors = repo.GetAll(author=>author.Books)
                .Where(author => author.Id != id).Take(6).ToList();
            AuthorDetailsViewModel authorDetails = new AuthorDetailsViewModel()
            {
                Author = author,
                RelatedAuthors = relatedAuthors,
            };
            return View(authorDetails);
        }
    }
}
