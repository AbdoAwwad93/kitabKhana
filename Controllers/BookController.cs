using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IRepository<Book> bookRepo;

        public BookController(IRepository<Book> BookRepo)
        {
            bookRepo=BookRepo;
        }
        public IActionResult Index()
        {
            var books = bookRepo.GetAll();
            return View(books);
        }
        public IActionResult Details(string Id)
        {
            var book = bookRepo.GetById(Id);
            if(book == null)
            {
                return Content("Not found");
            }
            return View(book);
        }
    }
}
