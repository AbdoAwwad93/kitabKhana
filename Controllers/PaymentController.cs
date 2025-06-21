using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IRepository<Customer> customerRepo, IOptions<StripeSettings> stripeSettings)
        {
            _customerRepo = customerRepo;
            _stripeSettings = stripeSettings.Value;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var customer = _customerRepo.GetById(userId);
            if (customer?.Cart == null || !customer.Cart.Books.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(customer.Cart);
        }

        public IActionResult CreateCheckoutSession()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _customerRepo.GetById(userId);

            if (customer?.Cart == null || !customer.Cart.Books.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var lineItems = new List<SessionLineItemOptions>();
            foreach (var book in customer.Cart.Books)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = book.Price * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = book.Title,
                        },
                    },
                    Quantity = 1,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Payment", null, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Payment", null, Request.Scheme),
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _customerRepo.GetById(userId);

            customer.Cart.Books.Clear();
            _customerRepo.SaveChanges();

            return View();
        }

        public IActionResult Cancel()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _customerRepo.GetById(userId);
            ViewBag.CartCount = customer?.Cart?.Books?.Count ?? 0;

            return View();
        }
    }
}
