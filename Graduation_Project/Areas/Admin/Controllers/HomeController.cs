using Domain;
using Domain.Models;
using Domain.Services;
using Domain.ViewModels;
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
            // Users
            var registrationRequests = _unitOfWork.TdRegistrationRequests.GetLatestWeek().ToArray();

            RegistrationRequestsVM model = new();
            model.RegistrationCountArray = new int[7];
            for (int i = 0; i < 7; i++)
            {
                model.RegistrationCountArray[i] = registrationRequests[6 - i].TotalRegistrations;
            }

            double average = ((double)(model.RegistrationCountArray[6] / (double)model.RegistrationCountArray[0]) * 100);
            if (average == 1)
                model.Average = 0;
            else
                model.Average = average;

            int usersCount = await _userService.UserRegistrationCount();
            double doctorsCount = await _unitOfWork.TbDoctors.CountAsync();
            double patientsCount = usersCount - doctorsCount;

            ViewBag.TotalRegistrations = usersCount;
            ViewBag.DoctorAverage = ((doctorsCount / usersCount) * 100);
            ViewBag.PatientAverage = ((patientsCount / usersCount) * 100);

            ///////////////////



            return View(model);
        }
    }
}
