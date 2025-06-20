using System.Diagnostics;
using Bookstore.Models;
using Bookstore.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Book> bookRepo;

        public HomeController(ILogger<HomeController> logger, IRepository<Book> bookRepo)
        {
            _logger = logger;
            this.bookRepo=bookRepo;
        }
        public IActionResult Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            int cartCount = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                var customerRepo = (Bookstore.Repositories.IRepository<Bookstore.Models.Customer>)HttpContext.RequestServices.GetService(typeof(Bookstore.Repositories.IRepository<Bookstore.Models.Customer>));
                var user = customerRepo?.GetById(userId);
                cartCount = user?.Cart?.Books?.Count ?? 0;
            }
            ViewBag.CartCount = cartCount;
            return View(bookRepo.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
