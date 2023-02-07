using Domain;
using Domain.ViewModels;
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



    }
}
