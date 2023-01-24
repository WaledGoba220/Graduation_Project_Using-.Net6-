using Domain.Models;
using Domain.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Utility.Consts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Advice_Service;
using System.Drawing.Printing;

namespace Graduation_Project.Controllers
{
    public class AdviceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private IAdviceService _adviceService;
        public AdviceController(UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IAdviceService adviceService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _adviceService = adviceService;
        }


        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 6)
        {
            ViewData["itemsCount"] = await _unitOfWork.TbAdvices.CountAsync();
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var currentUser = await GetCurrentUser();
            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();
            ViewBag.UserName = currentUser.UserName;
            ViewBag.UserPhoto = currentUser.Photo;

            AdvicesVM model = new();
            model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesAsync(pageSize, ExcludeRecords);
            model.Pagination = new PagePagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            return View(model);
        }

        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> MyAdvices(int pageNumber = 1, int pageSize = 6)
        {
            ViewData["itemsCount"] = await _unitOfWork.TbAdvices.CountAsync();
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var currentUser = await GetCurrentUser();
            int doctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);
            ViewBag.doctorId = doctorId;
            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();
            ViewBag.UserName = currentUser.UserName;
            ViewBag.UserPhoto = currentUser.Photo;

            MyAdviceVM model = new();
            model.LstAdvices = await _unitOfWork.TbAdvices.GetMyAdvicesAsync(doctorId, pageSize, ExcludeRecords);
            model.Pagination = new PagePagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            return View(model);
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        public async Task<IActionResult> SaveAdvice(MyAdviceVM model, IFormFile file)
        {
            if (!ModelState.IsValid)
            { 
                ViewData["itemsCount"] = await _unitOfWork.TbAdvices.CountAsync();
                int pageNumber = 1, pageSize = 6;
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var currentUser = await GetCurrentUser();
                int doctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);
                ViewBag.doctorId = doctorId;
                ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();
                ViewBag.UserName = currentUser.UserName;
                ViewBag.UserPhoto = currentUser.Photo;

                model.LstAdvices = await _unitOfWork.TbAdvices.GetMyAdvicesAsync(doctorId, pageSize, ExcludeRecords);
                model.Pagination = new PagePagination()
                {
                    pageNumber = pageNumber,
                    pageSize = pageSize
                };

                if (file.Length == 0)
                {
                    TempData["Error"] = "Please Upload Image";
                }else
                {
                    TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                }

                return View("MyAdvices", model);
            }

            if (model.AdviceVM is null || model.AdviceVM.Id == 0)
            {
                // Add
                var addAdvice = await _adviceService.AddAsync(model.AdviceVM, file);
                if (addAdvice.Success)
                    TempData["Success"] = addAdvice.Message;
                else
                    TempData["Error"] = addAdvice.Message;
            }
            else
            {
                // Edit
                var updateAdvice = await _adviceService.UpdateAsync(model.AdviceVM, file);
                if (updateAdvice.Success)
                    TempData["Success"] = updateAdvice.Message;
                else
                    TempData["Error"] = updateAdvice.Message;
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

        [HttpPost]
        public async Task<IActionResult> Search(AdvicesVM model)
        {
            int pageNumber = 1, pageSize = 6;
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var currentUser = await GetCurrentUser();
            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();
            ViewBag.UserName = currentUser.UserName;
            ViewBag.UserPhoto = currentUser.Photo;

            model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesBySearchFormAsync(model.SearchAdvice, pageSize, ExcludeRecords);
            ViewData["itemsCount"] = model.LstAdvices.Count();
            model.Pagination = new PagePagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            TempData["TitlePage"] = "Search";

            return View("Index", model);
        }



        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
