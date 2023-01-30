using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utility.Consts;

namespace Graduation_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;
        public UserController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(string userName)
        {
            UserProfileVM model = new();
            model.User = await _userManager.FindByNameAsync(userName);
            
            if(await _userManager.IsInRoleAsync(model.User, Roles.Doctor))
            {
                model.Doctor = await _unitOfWork.TbDoctors.GetFirstOrDefaultAsync(a => a.AppUserId == model.User.Id, new[] { "Specialization" });
                model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesByDocitorIdAsync(model.Doctor.Id);

                var doctorViews = await _unitOfWork.TbDoctorViewsCounts.GetFirstOrDefaultAsync(a => a.DoctorId == model.Doctor.Id);
                if(doctorViews is null)
                {
                    TbDoctorViewsCount view = new()
                    {
                        DoctorId = model.Doctor.Id,
                        Count = 1
                    };

                    await _unitOfWork.TbDoctorViewsCounts.AddAsync(view);
                }
                else
                {
                    doctorViews.Count++;
                    _unitOfWork.TbDoctorViewsCounts.Update(doctorViews);
                }
            }

            await _unitOfWork.Complete();

            return View(model);
        }
    
    }
}
