using DataAccess_EF;
using Domain;
using Domain.Models;
using Domain.Services;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Utility.Consts;

namespace Graduation_Project.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUnitOfWork _unitOfWork;
        private IUserService _userService;
        public ProfileController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUser();

            ProfilePageVM model = new();
            model.User = currentUser;
            if (User.IsInRole(Roles.Doctor))
            {
                bool doctor = await _unitOfWork.TbDoctors.FindAsync(a => a.AppUserId == currentUser.Id);
                if(doctor == true)
                {
                    model.Doctor = await _unitOfWork.TbDoctors.GetFirstOrDefaultAsync(a => a.AppUserId == currentUser.Id, new[] { "Specialization", "ClinicImages", "Pneumonias", "Tuberculosis", "LungCancers" });
                    model.ViewsCount = await _unitOfWork.TbDoctorViewsCounts.GetFirstOrDefaultAsync(a => a.DoctorId == model.Doctor.Id);

                    var getRatingByDoctorId = await _unitOfWork.TbRatings.GetWhereAsync(a => a.DoctorId == model.Doctor.Id);
                    if (getRatingByDoctorId is not null)
                    {
                        int ratingCount = getRatingByDoctorId.Count();
                        model.CalculateRating = getRatingByDoctorId.Sum(a => a.Rate) / ratingCount;
                        ViewBag.RatingCount = ratingCount;
                    }

                }

                ViewBag.LstSpecialization = await _unitOfWork.TbSpecialization.GetAllAsync();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserPhoto(IFormFile myFile)
        {
            var currentUser = await GetCurrentUser();

            var updatePhoto = await _userService.UpdateUserPhotoAsync(currentUser, myFile);
            if (updatePhoto.Success)
                TempData["Success"] = updatePhoto.Message;
            else
                TempData["Error"] = updatePhoto.Message;

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(string userName, string fullName)
        {
            var currentUser = await GetCurrentUser();

            var updatePatientInfo = await _userService.UpdateUserInfoAsync(userName, fullName, currentUser);
            if (updatePatientInfo.Success)
                TempData["Success"] = updatePatientInfo.Message;
            else
                TempData["Error"] = updatePatientInfo.Message;

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize(Roles =Roles.Doctor)]
        [HttpPost]
        public async Task<IActionResult> SaveDoctor(ProfilePageVM model)
        {

            if (!ModelState.IsValid)
            {
                var currentUser = await GetCurrentUser();
                model.User = currentUser;
                model.Doctor = await _unitOfWork.TbDoctors.GetFirstOrDefaultAsync(a => a.AppUserId == currentUser.Id, new[] { "Specialization", "ClinicImages" });
                model.ViewsCount = await _unitOfWork.TbDoctorViewsCounts.GetFirstOrDefaultAsync(a => a.DoctorId == model.Doctor.Id);

                TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                ViewBag.LstSpecialization = await _unitOfWork.TbSpecialization.GetAllAsync();
                return View("Index", model);
            }

            if(model.Doctor is null || model.Doctor.Id == 0)
            {
                // Add
                await _unitOfWork.TbDoctors.AddAsync(model.Doctor);
                TempData["Success"] = "Update Information Successfully!";
            }
            else
            {
                // Edit
                var doctor = await _unitOfWork.TbDoctors.GetFirstOrDefaultAsync(x=>x.Id == model.Doctor.Id);
                doctor.Location = model.Doctor.Location;
                doctor.Clinic = model.Doctor.Clinic;
                doctor.Phone = model.Doctor.Phone;
                doctor.Country = model.Doctor.Country;
                doctor.City = model.Doctor.City;
                doctor.Brief = model.Doctor.Brief;
                doctor.SpecializationId = model.Doctor.SpecializationId;

                _unitOfWork.TbDoctors.Update(doctor);
                TempData["Success"] = "Update Information Successfully!";
            }

            await _unitOfWork.Complete();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        public async Task<IActionResult> AddClinicImages(int doctorId, IFormFile clinicImage)
        {
            // Convert Photo from IFormFile to byte[]
            using var dataStream = new MemoryStream();
            await clinicImage.CopyToAsync(dataStream);

            TbClinicImage model = new()
            {
                Image = dataStream.ToArray(),
                DoctorId = doctorId,
            };

            await _unitOfWork.TbClinicImages.AddAsync(model);
            await _unitOfWork.Complete();
            TempData["Success"] = "Add New Clinic Photo Successfully!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        public async Task<IActionResult> DeleteClinicImage(string id)
        {
            var arr = id.Split("--");
            int itemId = Convert.ToInt32(arr[0]);
            int doctorId = Convert.ToInt32(arr[1]);

            var getItemById = await _unitOfWork.TbClinicImages.GetFirstOrDefaultAsync(a => a.Id == itemId && a.DoctorId == doctorId);
            if(getItemById is not null)
            {
                _unitOfWork.TbClinicImages.Delete(getItemById);
                await _unitOfWork.Complete();
                TempData["Success"] = "Delete Photo Successfully!";

                return Json(new { success = true });
            }

            return Json(new { success = false, message = "There is no photo to delete it, try again" });
        }
        
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
