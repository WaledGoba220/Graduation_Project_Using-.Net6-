using Domain.Models;
using Domain.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using E_Exam.Utility.EmailSender;
using Domain.ViewModels;
using Utility.Consts;
using E_Exam.Core.ViewModels;
using Hangfire;

namespace Graduation_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUserService _userService;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userService = userService;
        }


        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User)) //verify if it's logged
            {
                return LocalRedirect("~/");
            }
            return View();
        }

        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User)) //verify if it's logged
            {
                return LocalRedirect("~/");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, string role)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return View(model);
            }

            var findByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (findByEmail is not null)
            {
                TempData["Error"] = "This Email already Existing";
                return View(model);
            }

            var findByUsername = await _userManager.FindByNameAsync(model.UserName);
            if (findByUsername is not null)
            {
                TempData["Error"] = "This Username already Existing";
                return View(model);
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var createUser = await _userService.RegisterAsync(user, model.Password);
            if (createUser.Success)
            {   
                // Add User To Role Doctor
                if (role == Roles.Doctor)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Doctor);
                }

                // Create Link
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new { token = token, userId = user.Id }, Request.Scheme);
                // Send Email
                string formatUrl = $"<h3> To confirm email, you should <a href='{url}'> Click here </a> </h3>";
                _emailSender.SendEmail(user.Email, "Email Confirmation", formatUrl);

                TempData["Success"] = createUser.Message;
                return RedirectToAction("LogIn");
            }
            else
            {
                TempData["Error"] = createUser.Message;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return View(model);
            }

            var result = await _userService.LoginAsync(model);
            if (result.Success)
            {
                TempData["Success"] = result.Message;

                if (!string.IsNullOrEmpty(returnUrl))
                    return LocalRedirect(returnUrl);
                else
                    return Redirect("/");
            }
            else
            {
                TempData["Error"] = result.Message;
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Success"] = "LogOut Successfully!";
            return Redirect("/");
        }

        public async Task<IActionResult> ConfirmEmail(ConfirmEmailVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ConfirmEmailAsync(model);
                if (result.Success)
                    TempData["Success"] = result.Message;
                else
                    TempData["Error"] = result.Message;
            }

            return RedirectToAction("LogIn");
        }

    }
}
