using Domain;
using Domain.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Graduation_Project.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            HomePageVM model = new();
            model.LatestAdvices = await _unitOfWork.TbAdvices.GetLatestAdvices();

            return View(model);
        }


        // Switch Between Cultures
        public IActionResult SetCulture(string lang, string returnUrl)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }
            return Redirect(returnUrl);
        }
    }
}