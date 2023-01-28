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

            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

            AdvicesVM model = new();
            model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesAsync(pageSize, ExcludeRecords);
            model.Pagination = new PagePagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            return View(model);
        }

        public async Task<IActionResult> AdviceDetails(int id)
        {
            bool findAdviceById = await _unitOfWork.TbAdvices.FindAsync(a => a.Id == id);
            if (findAdviceById == true)
            {
                var currentUser = await GetCurrentUser();
                ViewBag.UserId = currentUser.Id;
                ViewBag.AdviceId = id;

                AdviceDetailsVM model = new();
                model.Advice = await _unitOfWork.TbAdvices.GetFirstOrDefaultAsync(a => a.Id == id, new[] { "DiseaseType", "Disease", "Doctor", "Comments" });
                model.LstReplays = await _unitOfWork.TbReplays.GetWhereAsync(a => a.AdviceId == id, new[] { "AppUser" });
                model.LstComments = await _unitOfWork.TbComments.GetWhereAsync(a => a.AdviceId == id, new[] { "AppUser" });

                var getSpecializationById = await _unitOfWork.TbSpecialization.GetFirstOrDefaultAsync(a => a.Id == model.Advice.Doctor.SpecializationId);

                ViewBag.Specialization = getSpecializationById.Name_EN;

                return View(model);
            }
            else
            {
                TempData["Error"] = @"Not Found Advice with ID: {id}";
                return RedirectToAction("Index");
            }
        }


        [Authorize(Roles = Roles.Doctor)]
        public async Task<IActionResult> MyAdvices(int pageNumber = 1, int pageSize = 6)
        {
            ViewData["itemsCount"] = await _unitOfWork.TbAdvices.CountAsync();
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var currentUser = await GetCurrentUser();
            ViewBag.UserId = currentUser.Id;
            int doctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);
            ViewBag.doctorId = doctorId;
            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

            MyAdviceVM model = new();
            model.LstAdvices = await _unitOfWork.TbAdvices.GetMyAdvicesAsync(doctorId, pageSize, ExcludeRecords);
            model.Pagination = new PagePagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search(AdvicesVM model)
        {
            int pageNumber = 1, pageSize = 6;
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

            if (model.SearchAdvice.DiseaseTypeId != 0 && !String.IsNullOrEmpty(model.SearchAdvice.Title))
            {
                model.SearchAdvice.Diseases = await _unitOfWork.TbDiseases.GetWhereAsync(a => a.DiseaseTypeId == model.SearchAdvice.DiseaseTypeId);
                model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesBySearchFormAsync(model.SearchAdvice, pageSize, ExcludeRecords);
            }
            else if (model.SearchAdvice.DiseaseTypeId != 0)
            {
                model.SearchAdvice.Diseases = await _unitOfWork.TbDiseases.GetWhereAsync(a => a.DiseaseTypeId == model.SearchAdvice.DiseaseTypeId);
                model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesByDiseaseTypeAndDisease(model.SearchAdvice.DiseaseTypeId, model.SearchAdvice.DiseaseId, pageSize, ExcludeRecords);
            }
            else if (!String.IsNullOrEmpty(model.SearchAdvice.Title))
            {
                model.LstAdvices = await _unitOfWork.TbAdvices.GetAdvicesByTitle(model.SearchAdvice.Title, pageSize, ExcludeRecords);
            }
            else
            {
                TempData["Warning"] = "Not Found Advices, Please Try Again";
                return RedirectToAction("Index");
            }


            ViewData["itemsCount"] = model.LstAdvices.Count();
            model.Pagination = new PagePagination()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            };

            TempData["TitlePage"] = "Search";
            return View("Index", model);
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
                ViewBag.UserId = currentUser.Id;
                ViewBag.LstDiseaseTypes = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveComment(AdviceDetailsVM model)
        {
            if(String.IsNullOrEmpty(model.Comment.Comment))
            {
                TempData["Error"] = "You Must Write Comment";
            }
            else
            {
                try
                {
                    await _unitOfWork.TbComments.AddAsync(model.Comment);
                    await _unitOfWork.Complete();

                    TempData["Success"] = "Add Comment Successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return Redirect("~/Advice/AdviceDetails/" + model.Comment.AdviceId);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveReplay(AdviceDetailsVM model)
        {
            if (String.IsNullOrEmpty(model.Replay.Replay))
            {
                TempData["Error"] = "You Must Write Replay";
            }
            else
            {
                try
                {
                    await _unitOfWork.TbReplays.AddAsync(model.Replay);
                    await _unitOfWork.Complete();

                    TempData["Success"] = "Add Replay Successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return Redirect("~/Advice/AdviceDetails/" + model.Replay.AdviceId);
        }

        [Authorize(Roles =Roles.Doctor)]
        public async Task<IActionResult> DeleteAdvice(int id)
        {
            try
            {
                var getAdviceById = await _unitOfWork.TbAdvices.GetFirstOrDefaultAsync(a => a.Id == id);
                if(getAdviceById is not null)
                {
                    _unitOfWork.TbAdvices.Delete(getAdviceById);
                    await _unitOfWork.Complete();

                    TempData["Success"] = "Delete Advice Successfully!";
                }
                else
                {
                    TempData["Error"] = @"Not Found Advice By ID: {id}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

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
