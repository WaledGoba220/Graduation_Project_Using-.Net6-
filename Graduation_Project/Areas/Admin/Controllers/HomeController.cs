using Domain;
using Domain.Models;
using Domain.Services;
using Domain.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Consts;

namespace Graduation_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;
        public HomeController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            // Hangfire
            RecurringJob.AddOrUpdate(() => _unitOfWork.TbAdviceRequests.SaveTotalAdvicesEveryDay(), Cron.Daily);
            RecurringJob.AddOrUpdate(() => _userService.SaveTotlaRegistrationsEveryDay(), Cron.Daily);

            DashboardPageVM model = new();
            model.RegistrationsRequests = await _unitOfWork.TdRegistrationRequests.RegistrationRequestsAsync();
            model.AdvicesRequests = await _unitOfWork.TbAdviceRequests.AdviceRequestsAsync();

            return View(model);
        }
    }
}
