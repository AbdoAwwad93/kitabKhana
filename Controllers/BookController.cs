using Bookstore.Models;
using Bookstore.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            var user = customerRepo.GetById(userId);
            cartCount = user?.Cart?.Books?.Count ?? 0;

            ViewBag.CartCount = cartCount;
            var books = bookRepo.GetAll();
            return View(books);
        }
        public IActionResult Details(string Id)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            int cartCount = 0;
            var user = customerRepo.GetById(userId);
            cartCount = user?.Cart?.Books?.Count ?? 0;
            ViewBag.CartCount = cartCount;
            var book = bookRepo.GetById(Id);
            if (book == null)
            {
                return Content("Not found");
            }

            bool isInCart = false;
            if (user?.Cart?.Books != null)
            {
                isInCart = user.Cart.Books.Any(b => b.Id == Id);
            }
         
            var relatedBooks = bookRepo.GetAll()
                .Where(b => b.Category == book.Category && b.Id != book.Id)
                .ToList();
            var viewModel = new BookDetailsViewModel
            {
                Book = book,
                RelatedBooks = relatedBooks,
                IsInCart = isInCart
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string commentText,int Rate,string BookId)
        {
            var user = customerRepo.GetById(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (user == null)
            {
                return Unauthorized();
            }

            var book = bookRepo.GetById(BookId);
            if (book == null)
            {
                return NotFound();
            }

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
            return PartialView("_CommentPartial", comment);
        }
    }
}
