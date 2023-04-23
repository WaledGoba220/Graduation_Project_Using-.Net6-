using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.Consts;

namespace Graduation_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class LungTransplantController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public LungTransplantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var items = await _unitOfWork.TbLungTransplant.GetMainDataAsync();

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var getItemById = await _unitOfWork.TbLungTransplant.GetFirstOrDefaultAsync(a => a.Id == id);
            getItemById.Status = status;

            _unitOfWork.TbLungTransplant.Update(getItemById);
            await _unitOfWork.Complete();

            TempData["Success"] = "ChangeStatus Successfully!";

            return RedirectToAction("Index");
        }

    }
}
