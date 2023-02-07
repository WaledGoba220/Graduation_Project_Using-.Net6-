using Domain;
using Domain.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Consts;

namespace Graduation_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class AdviceController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public AdviceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            List<AdviceVM> model = await _unitOfWork.TbAdvices.GetAllAdvicesAsync();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAdvice(int id)
        {
            var getAdviceById = await _unitOfWork.TbAdvices.GetFirstOrDefaultAsync(a => a.Id == id);
            if (getAdviceById is not null)
            {
                _unitOfWork.TbAdvices.Delete(getAdviceById);
                await _unitOfWork.Complete();

                TempData["Success"] = "Delete Advice Successfully!";

                return Json(new { success = true });
            }
            else
            {
                TempData["Error"] = $"Not Found ADvice with ID: {id}";

                return Json(new { success = false });
            }
        }

    }
}
