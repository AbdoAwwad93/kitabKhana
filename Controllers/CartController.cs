using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;

namespace Bookstore.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Customer> customerRepo;
        private readonly IRepository<Book> bookRepo;

        public CartController(IRepository<Customer> customerRepo,IRepository<Book> BookRepo)
        {
            this.customerRepo=customerRepo;
            bookRepo=BookRepo;

        }
        public IActionResult Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            if(userId == null)
            {
              return RedirectToAction("Login", "Account", new { ReturnUrl = Url.Action("Index", "Cart") });
            }
            var user = customerRepo.GetById(userId);
            if(user == null)
            {
              return RedirectToAction("Login", "Account", new { ReturnUrl = Url.Action("Index", "Cart") });
            }
            var cartCount = user?.Cart?.Books?.Count ?? 0;
            ViewBag.CartCount = cartCount;
            return View(user); 
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] string id)
        {
            var book = bookRepo.GetById(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = customerRepo.GetById(userId);
            if (user == null)
            {
                Response.StatusCode = 401;
                return Json(new { success = false, message = "User not logged in" });
            }
            if (user.Cart == null)
            {
                user.Cart = new Cart { CustomerId = user.Id, Books = new List<Book>() };
            }
            if (user.Cart.Books == null)
            {
                user.Cart.Books = new List<Book>();
            }
            if (!user.Cart.Books.Any(b => b.Id == id))
            {
                user.Cart.Books.Add(book);
                customerRepo.SaveChanges();
            }
            var count = user.Cart.Books.Count;
            return Json(new { success = true, count });
        }
        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] string id)
        {
            var user = customerRepo.GetById(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var book = bookRepo.GetById(id);
            user.Cart.Books.Remove(book);
            customerRepo.SaveChanges();
            var count = user.Cart.Books.Count;
            return Json(new { success = true, count });
        }
    }
}
