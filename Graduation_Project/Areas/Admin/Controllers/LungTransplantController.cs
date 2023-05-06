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

        public async Task<IActionResult> RequestDetails(int id)
        {
            var item = await _unitOfWork.TbLungTransplant.GetFirstOrDefaultAsync(a => a.Id == id);
            if (item is not null)
            {
                return View(item);
            }

            TempData["Error"] = "Not found request with ID: " + id + "";

            return View();
        }

        public async Task<IActionResult> AnalysisFile(int id)
        {
            var selectedFile = await _unitOfWork.TbLungAnalysisFile.GetFirstOrDefaultAsync(a => a.LungTransplantId == id);
            if (selectedFile is not null)
            {
                var path = "~/Uploads/" + selectedFile.FileName;

                // Clear Cache
                Response.Headers.Add("Expires", DateTime.Now.AddDays(-3).ToLongDateString());
                Response.Headers.Add("Cache-Control", "no-cache");

                return File(path, selectedFile.ContentType);
            }

            TempData["Error"] = "Not found this file, Please try again";

            return Redirect(Request.Headers["Referer"].ToString());
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
