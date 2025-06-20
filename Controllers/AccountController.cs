using Bookstore.Models;
using Bookstore.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;

        public AccountController(UserManager<Customer> userManager,SignInManager<Customer> signInManager)
        {
            this.userManager=userManager;
            this.signInManager=signInManager;
        }
        private async Task SetCartCountAsync()
        {
            int cartCount = 0;
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                cartCount = user.Cart?.Books?.Count ?? 0;
            }
            ViewBag.CartCount = cartCount;
        }
        public IActionResult SignUpView()
        {
            SetCartCountAsync().Wait();
            return View("SignUp");
        }
        public async Task<IActionResult> SignUp(SignUpViewModel signUpModel)
        {
            SetCartCountAsync().Wait();
            var user = await userManager.FindByEmailAsync(signUpModel.Email);
            if(user != null)
            {
                ModelState.AddModelError("", "Account Already exists");
                return View("SignUp", signUpModel);
            }
            if (ModelState.IsValid)
            {
                var Customer = new Customer()
                {
                    CustomerName = signUpModel.FullName,
                    UserName = signUpModel.UserName,
                    Email = signUpModel.Email,
                    RegisterationDate = DateTime.Now,
                    PhoneNumber = signUpModel.PhoneNumber,
                    Address = signUpModel.Address,
                };
              var result= await userManager.CreateAsync(Customer,signUpModel.Password);
                if (result.Succeeded)
                {

                    return View("Login");  
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View("SignUp",signUpModel);
          
        }
        public IActionResult LoginView()
        {
            SetCartCountAsync().Wait();
            return View("Login");
        }
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            SetCartCountAsync().Wait();
            if (ModelState.IsValid)
            {
                var user  = await userManager.FindByEmailAsync(loginModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong Email or Password");
                    return View("Login", loginModel);
                }
                var pass = await userManager.CheckPasswordAsync(user, loginModel.Password);
                if (!pass)
                {
                    ModelState.AddModelError("", "Wrong Email or Password");
                    return View("Login", loginModel);
                }
                await signInManager.SignInAsync(user, isPersistent: loginModel.RememberMe);
                
                return RedirectToAction("Index", "Home"); 
            }
            return View("Login", loginModel);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Profile()
        {
            SetCartCountAsync().Wait();
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("LoginView");
            }
            var profileModel = new ProfileViewModel()
            {
                FullName = user.CustomerName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName=user.UserName
            };


            return View("Profile",profileModel);
        }
        public async Task<IActionResult> EditView()
        {
            SetCartCountAsync().Wait();
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("LoginView");
            }
            var profileModel = new ProfileViewModel()
            {
                FullName = user.CustomerName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName=user.UserName
            };
            return View("EditProfile",profileModel);
        }
        public async Task<IActionResult> Edit(ProfileViewModel profileModel)
        {
            SetCartCountAsync().Wait();
            var user = await userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToAction("LoginView");
            }
            if(ModelState.IsValid)
            {
                user.CustomerName = profileModel.FullName;
                user.Address = profileModel.Address;
                user.PhoneNumber = profileModel.PhoneNumber;
                user.Email = profileModel.Email;
                user.UserName = profileModel.UserName;
                var result = await userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("EditProfile",profileModel);

            
        }
       
    }
}
