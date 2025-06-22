using Bookstore.Models;
using Bookstore.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

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

        public IActionResult Delivery()
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

            var subtotal = customer.Cart.Books.Sum(book => book.Price);
            var deliveryCost = 25.0m;
            var total = subtotal + deliveryCost;

            var deliveryViewModel = new DeliveryViewModel
            {
                FullName = customer.CustomerName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                City = "القاهرة",
                DeliveryMethod = DeliveryMethod.Standard,
                Subtotal = subtotal,
                DeliveryCost = deliveryCost,
                Total = total
            };
            return View(deliveryViewModel);
        }

        [HttpPost]
        public IActionResult Delivery(DeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _customerRepo.GetById(userId);
            var subtotal = customer.Cart.Books.Sum(b => b.Price);
            
            model.DeliveryCost = model.DeliveryMethod switch
            {
                DeliveryMethod.Standard => 25.0m,
                DeliveryMethod.Express => 50.0m,
                DeliveryMethod.SameDay => 100.0m,
                _ => 25.0m
            };
            
            model.Subtotal = subtotal;
            model.Total = subtotal + model.DeliveryCost;

            TempData["DeliveryInfo"] = JsonSerializer.Serialize(model);
            return RedirectToAction("CreateCheckoutSession");
        }

        public IActionResult CreateCheckoutSession()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _customerRepo.GetById(userId);

            if (customer?.Cart == null || !customer.Cart.Books.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var deliveryInfoJson = TempData["DeliveryInfo"] as string;
            if (string.IsNullOrEmpty(deliveryInfoJson))
            {
                return RedirectToAction("Delivery");
            }

            var deliveryInfo = JsonSerializer.Deserialize<DeliveryViewModel>(deliveryInfoJson);
            if (deliveryInfo == null)
            {
                return RedirectToAction("Delivery");
            }

            var lineItems = new List<SessionLineItemOptions>();
            
            foreach (var book in customer.Cart.Books)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(book.Price * 100), 
                        Currency = "EGP",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = book.Title,
                            Description = "سعر الكتب"
                        },
                    },
                    Quantity = 1,
                });
            }
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(deliveryInfo.DeliveryCost * 100),                   Currency = "EGP",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"التوصيل - {GetDeliveryMethodName(deliveryInfo.DeliveryMethod)}",
                        Description = "رسوم التوصيل"
                    },
                },
                Quantity = 1,
            });

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
            if (customer?.Cart != null)
            {
                if(customer.PurchasedBooks ==null)
                {
                    customer.PurchasedBooks = new List<Book>();
                }
                customer.PurchasedBooks.AddRange(customer.Cart.Books);
                customer.Cart.Books.Clear();
                _customerRepo.SaveChanges();
            }
            return View();
        }

        public IActionResult Cancel()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var customer = _customerRepo.GetById(userId);
                ViewBag.CartCount = customer?.Cart?.Books?.Count ?? 0;
            }
            return View();
        }

        private string GetDeliveryMethodName(DeliveryMethod method)
        {
            return method switch
            {
                DeliveryMethod.Standard => "عادي (3-5 أيام)",
                DeliveryMethod.Express => "سريع (1-2 يوم)",
                DeliveryMethod.SameDay => "نفس اليوم",
                _ => "عادي"
            };
        }
    }
}
