using data_Access_layer.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class AccountController : Controller  // Changed to PascalCase to follow conventions
    {
        private readonly UserManager<appUser> _manager;
        private readonly SignInManager<appUser> _signInManager;

        public AccountController(UserManager<appUser> manager, SignInManager<appUser> signInManager)
        {
            _manager = manager;
            _signInManager = signInManager;
        }

        #region register

        public IActionResult Register()  // Changed to PascalCase
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerView)
        {
            if (ModelState.IsValid)
            {
                var user = new appUser()
                {
                    UserName = registerView.Email.Split('@')[0],
                    Email = registerView.Email,
                    IsAgree = registerView.IsAgree,
                    FirstName = registerView.FirstName,
                    LastName = registerView.LastName
                };
                var res = await _manager.CreateAsync(user, registerView.Password);
                if (res.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerView);
        }
        #endregion
        #region login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userlogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _manager.FindByEmailAsync(userlogin.Email);
                if (user != null)
                {
                    var passwordValid = await _manager.CheckPasswordAsync(user, userlogin.Password);
                    if (passwordValid)
                    {
                        var loginres = await _signInManager.PasswordSignInAsync(
                            user.UserName,  // Using UserName instead of the model
                            userlogin.Password,
                            userlogin.RememberMe,
                            false);

                        if (loginres.Succeeded)
                        {
                            return RedirectToAction("Index", "Employee");  // Fixed syntax error (missing semicolon)
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password is not correct");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email does not exist");
                }
            }
            return View(userlogin);  // Changed from RedirectToAction to View to show validation errors
        } 
        #endregion

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}