using Bookstore.Models;
using Bookstore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookstore.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Customer> _customerRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<CartItem> _cartItemRepo;
        private readonly IRepository<Book> _bookRepo;

        public CartController(
            IRepository<Customer> customerRepo,
            IRepository<Cart> cartRepo,
            IRepository<CartItem> cartItemRepo,
            IRepository<Book> bookRepo)
        {
            _customerRepo = customerRepo;
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _bookRepo = bookRepo;
        }

        public IActionResult Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var cart = GetOrCreateCart(userId);
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(string bookId, int quantity = 1)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Json(new { success = false, message = "يرجى تسجيل الدخول أولاً" });
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { success = false, message = "يرجى تسجيل الدخول أولاً" });
            }

            var book = _bookRepo.GetById(bookId);
            if (book == null)
            {
                return Json(new { success = false, message = "الكتاب غير موجود" });
            }

            var cart = GetOrCreateCart(userId);
            var existingItem = cart.CartItems.FirstOrDefault(i => i.BookId == bookId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.SubTotal = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid().ToString(),
                    CartId = cart.Id,
                    BookId = bookId,
                    Quantity = quantity,
                    UnitPrice = book.Price,
                    SubTotal = book.Price * quantity
                };
                cart.CartItems.Add(cartItem);
            }

            UpdateCartTotal(cart);
            _cartRepo.Update(cart);

            return Json(new { success = true, message = "تمت إضافة الكتاب إلى السلة" });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string cartItemId)
        {
            var cartItem = _cartItemRepo.GetById(cartItemId);
            if (cartItem == null)
            {
                return Json(new { success = false, message = "العنصر غير موجود في السلة" });
            }

            var cart = _cartRepo.GetById(cartItem.CartId);
            if (cart == null)
            {
                return Json(new { success = false, message = "السلة غير موجودة" });
            }

            cart.CartItems.Remove(cartItem);
            _cartItemRepo.Delete(cartItemId);
            
            UpdateCartTotal(cart);
            _cartRepo.Update(cart);

            return Json(new { success = true, message = "تم حذف العنصر من السلة" });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(string cartItemId, int quantity)
        {
            var cartItem = _cartItemRepo.GetById(cartItemId);
            if (cartItem == null)
            {
                return Json(new { success = false, message = "العنصر غير موجود في السلة" });
            }

            cartItem.Quantity = quantity;
            cartItem.SubTotal = cartItem.Quantity * cartItem.UnitPrice;
            _cartItemRepo.Update(cartItem);

            var cart = _cartRepo.GetById(cartItem.CartId);
            if (cart == null)
            {
                return Json(new { success = false, message = "السلة غير موجودة" });
            }

            UpdateCartTotal(cart);
            _cartRepo.Update(cart);

            return Json(new { success = true, message = "تم تحديث الكمية" });
        }

        private Cart GetOrCreateCart(string userId)
        {
            var cart = _cartRepo.GetAll()
                .FirstOrDefault(c => c.CustomerId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = userId,
                    CartItems = new List<CartItem>(),
                    CreatedAt = DateTime.UtcNow
                };
                _cartRepo.Add(cart);
            }

            return cart;
        }

        private void UpdateCartTotal(Cart cart)
        {
            cart.TotalPrice = cart.CartItems.Sum(item => item.SubTotal);
            cart.UpdatedAt = DateTime.UtcNow;
        }
    }
}
