using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IRepository<Author> repo;

        public AuthorController(IRepository<Author> _repo)
        {
            repo=_repo;
        }
        public IActionResult Index()
        {
            var authors = repo.GetAll();
            return View(authors);
        }
        public IActionResult Details(string id)
        {
            var author = repo.GetById(id);
            return View(author);
        }
    }
}
