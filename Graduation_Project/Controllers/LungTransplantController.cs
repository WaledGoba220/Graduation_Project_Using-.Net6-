using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.LungTransplant_Service;

namespace Graduation_Project.Controllers
{
    [Authorize]
    public class LungTransplantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ILungTransplantService _lungTransplantService;
        private IUnitOfWork _unitOfWork;
        public LungTransplantController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, ILungTransplantService lungTransplantService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _lungTransplantService = lungTransplantService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(LungTransplantVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return View("Index", model);
            }

            var currentUser = await GetCurrentUser();
            var result = await _lungTransplantService.AddRequestAsync(model, currentUser.Id);
            if (result.Success)
            {
                TempData["Success"] =result.Message;
                return View("Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return View("Index", model);
            }
        }


        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
