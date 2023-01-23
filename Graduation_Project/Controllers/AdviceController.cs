using Domain.Models;
using Domain.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Utility.Consts;

namespace Graduation_Project.Controllers
{
    public class AdviceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;
        public AdviceController(UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> MyAdvices()
        {
            var currentUser = await GetCurrentUser();
            int doctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);
            ViewBag.doctorId = doctorId;
            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

            MyAdviceVM model = new();
            model.LstAdvices = await _unitOfWork.TbAdvices.GetWhereAsync(a => a.DoctorId == doctorId);

            return View(model);
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        public async Task<IActionResult> SaveAdvice(MyAdviceVM model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                var currentUser = await GetCurrentUser();
                int doctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);
                ViewBag.doctorId = doctorId;
                ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

                TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return View("MyAdvices", model);
            }

            if (file.Length == 0)
            {
                TempData["Error"] = "Please Upload Image";
                return RedirectToAction("Index");
            }

            // Convert Photo from IFormFile to byte[]
            using var dataStream = new MemoryStream();
            await file.CopyToAsync(dataStream);

            if (model.AdviceVM is null || model.AdviceVM.Id == 0)
            {
                // Add
                TbAdvice dt = new()
                {
                    Image = dataStream.ToArray(),
                    Title = model.AdviceVM.Title,
                    Content = model.AdviceVM.Content,
                    DoctorId = model.AdviceVM.DoctorId,
                    DiseaseId = model.AdviceVM.DiseaseId,
                    DiseaseTypeId = model.AdviceVM.DiseaseTypeId
                };

                await _unitOfWork.TbAdvices.AddAsync(dt);
                TempData["Success"] = "Add New Advice Successfully!";
            }
            else
            {
                // Edit
                var advice = await _unitOfWork.TbAdvices.GetFirstOrDefaultAsync(x => x.Id == model.AdviceVM.Id);
                advice.Title = model.AdviceVM.Title;
                advice.Content = model.AdviceVM.Content;
                advice.Image = dataStream.ToArray();
                advice.DoctorId = model.AdviceVM.DoctorId;
                advice.DiseaseId = model.AdviceVM.DiseaseId;
                advice.DiseaseTypeId = model.AdviceVM.DiseaseTypeId;

                _unitOfWork.TbAdvices.Update(advice);
                TempData["Success"] = "Update Advice Successfully!";
            }

            await _unitOfWork.Complete();

            return RedirectToAction("MyAdvices");
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> GetDiseasesByDiseaseTypeId(int diseaseTypeId)
        {
            var diseases = await _unitOfWork.TbDiseases.GetWhereAsync(a => a.DiseaseTypeId == diseaseTypeId);

            return Ok(diseases);
        }





        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
