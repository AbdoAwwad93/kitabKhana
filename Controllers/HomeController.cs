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
        private readonly IRepository<Customer> customerRepo;

        public HomeController(ILogger<HomeController> logger, IRepository<Book> bookRepo,IRepository<Customer> customerRepo)
        {
            _logger = logger;
            this.bookRepo=bookRepo;
            this.customerRepo=customerRepo;
        }
        public IActionResult Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value : null;
            int cartCount = 0;
            var user = customerRepo?.GetById(userId);
            cartCount = user?.Cart?.Books?.Count ?? 0;
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
