using Domain;
using Domain.ViewModels;
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

    }
}