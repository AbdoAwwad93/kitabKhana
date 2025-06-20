using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IRepository<Book> bookRepo;
        private readonly IRepository<Customer> customerRepo;

        public BookController(IRepository<Book> BookRepo, IRepository<Customer> customerRepo)
        {
            bookRepo = BookRepo;
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
            var books = bookRepo.GetAll();
            return View(books);
        }
        public IActionResult Details(string Id)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            int cartCount = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = customerRepo.GetById(userId);
                cartCount = user?.Cart?.Books?.Count ?? 0;
            }
            ViewBag.CartCount = cartCount;
            var book = bookRepo.GetById(Id);
            if(book == null)
            {
                return Content("Not found");
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult AddComment(string commentText,int Rate,string BookId)
        {
            var book = bookRepo.GetById(BookId);
            var user = customerRepo.GetById(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var comment = new Comment
            {
                Id=Guid.NewGuid().ToString(),
                CommentText = commentText,
                Rate = Rate,
                BookId = BookId,
                CustomerName = user.CustomerName,
                CreatedAt = DateTime.Now
            };
            if(book.Comments ==null)
            {
                book.Comments = new List<Comment>();
            }
            book.Comments.Add(comment);
            bookRepo.SaveChanges();
            return RedirectToAction("Details",book);
        }
    }
}
