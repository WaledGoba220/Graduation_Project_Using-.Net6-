using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Consts;

namespace Graduation_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class DiseaseController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public DiseaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<TbDisease> model = await _unitOfWork.TbDiseases.GetAllAsync(new[] { "DiseaseType" });

            return View(model);
        }
    
        public async Task<IActionResult> DiseaseType()
        {
            IEnumerable<TbDiseaseType> model = await _unitOfWork.TbDiseaseTypes.GetAllAsync(new[] { "Diseases" });

            return View(model);
        }

        public async Task<IActionResult> EditDisease(int id)
        {
            ViewBag.AllDiseaseType = await _unitOfWork.TbDiseaseTypes.GetAllAsync();

            if (id != 0)
            {
                var result = await _unitOfWork.TbDiseases.GetFirstOrDefaultAsync(a => a.Id == id);

                DiseaseVM model = new();
                model.Id = result.Id;
                model.Name_EN = result.Name_EN;
                model.Name_AR = result.Name_AR;
                model.DiseaseTypeId = result.DiseaseTypeId;

                return View(model);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> EditDiseaseType(int id)
        {
            if (id != 0)
            {
                var result = await _unitOfWork.TbDiseaseTypes.GetFirstOrDefaultAsync(a => a.Id == id);

                DiseaseTypeVM model = new();
                model.Id = result.Id;
                model.Name_EN = result.Name_EN;
                model.Name_AR = result.Name_AR;

                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveDisease(DiseaseVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllDiseaseType = await _unitOfWork.TbDiseaseTypes.GetAllAsync();
                return View("EditDisease", model);
            }

            if (model.Id == 0)
            {
                // Add
                TbDisease tbDisease = new()
                {
                    Name_AR = model.Name_AR,
                    Name_EN = model.Name_EN,
                    DiseaseTypeId = model.DiseaseTypeId
                };

                await _unitOfWork.TbDiseases.AddAsync(tbDisease);
                TempData["Success"] = "Add New Disease Successfully!";
            }
            else
            {
                // Edit
                TbDisease tbDisease = await _unitOfWork.TbDiseases.GetFirstOrDefaultAsync(a => a.Id == model.Id);
                tbDisease.Name_AR = model.Name_AR;
                tbDisease.Name_EN = model.Name_EN;
                tbDisease.DiseaseTypeId = model.DiseaseTypeId;

                _unitOfWork.TbDiseases.Update(tbDisease);
                TempData["Success"] = "Update Disease Successfully!";
            }

            await _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SaveDiseaseType(DiseaseTypeVM model)
        {
            if (!ModelState.IsValid)
                return View("EditDiseaseType", model);

            if(model.Id == 0)
            {
                // Add
                TbDiseaseType tbDiseaseType = new()
                {
                    Name_AR = model.Name_AR,
                    Name_EN = model.Name_EN
                };

                await _unitOfWork.TbDiseaseTypes.AddAsync(tbDiseaseType);
                TempData["Success"] = "Add New Type Successfully!";
            }
            else
            {
                // Edit
                TbDiseaseType tbDiseaseType = await _unitOfWork.TbDiseaseTypes.GetFirstOrDefaultAsync(a => a.Id == model.Id);
                tbDiseaseType.Name_AR = model.Name_AR;
                tbDiseaseType.Name_EN = model.Name_EN;

                _unitOfWork.TbDiseaseTypes.Update(tbDiseaseType);
                TempData["Success"] = "Update Type Successfully!";
            }

            await _unitOfWork.Complete();
            return RedirectToAction("DiseaseType");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDiseaseType(int id)
        {
            var getDiseaseTypeById = await _unitOfWork.TbDiseaseTypes.GetFirstOrDefaultAsync(a => a.Id == id);
            if(getDiseaseTypeById is not null)
            {
                _unitOfWork.TbDiseaseTypes.Delete(getDiseaseTypeById);
                await _unitOfWork.Complete();

                TempData["Success"] = "Delete Type Successfully!";

                return Json(new { success = true });
            }
            else
            {
                TempData["Error"] = $"Not Found Type with ID: {id}";

                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var getDiseaseById = await _unitOfWork.TbDiseases.GetFirstOrDefaultAsync(a => a.Id == id);
            if (getDiseaseById is not null)
            {
                _unitOfWork.TbDiseases.Delete(getDiseaseById);
                await _unitOfWork.Complete();

                TempData["Success"] = "Delete Disease Successfully!";

                return Json(new { success = true });
            }
            else
            {
                TempData["Error"] = $"Not Found Disease with ID: {id}";

                return Json(new { success = false });
            }
        }
    }
}
