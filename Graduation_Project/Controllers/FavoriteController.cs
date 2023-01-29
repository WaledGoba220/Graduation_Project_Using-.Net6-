using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    public class FavoriteController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public FavoriteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            FavoritePageVM model = HttpContext.Session.Get<FavoritePageVM>("Favorite");

            if (model is not null)
            {
                ViewData["itemsCount"] = model.LstAdvices.AsQueryable().Count();
                return View(model);
            }

            return View();
        }

        public async Task<IActionResult> AddToFavorite(int id)
        {
            FavoritePageVM model = HttpContext.Session.Get<FavoritePageVM>("Favorite");

            if (model == null)
                model = new FavoritePageVM();

            TbAdvice item = await _unitOfWork.TbAdvices.GetFirstOrDefaultAsync(a => a.Id == id, new[] { "AppUser", "Comments" });
            TbDiseaseType diseaseType = await _unitOfWork.TbDiseaseTypes.GetFirstOrDefaultAsync(a => a.Id == item.DiseaseTypeId);
            TbDisease disease = await _unitOfWork.TbDiseases.GetFirstOrDefaultAsync(a => a.Id == item.DiseaseId);

            FavoriteItemsVM getItemById = model.LstAdvices.FirstOrDefault(a => a.Id == id);
            if(getItemById is not null)
            {
                TempData["Error"] = "This Advice Already Added To Favorite";

                return ViewComponent("Favorite");
            }
            else
            {
                model.LstAdvices.Add(new FavoriteItemsVM()
                {
                    Id = item.Id,
                    Image = item.Image,
                    Title = item.Title,
                    Content = item.Content,
                    CreationDateTime = item.CreationDateTime,
                    CommentsCount = item.Comments.AsQueryable().Count(),
                    UserId = item.AppUserId,
                    User = item.AppUser,
                    DiseaseTypeName_EN = diseaseType.Name_EN,
                    DiseaseTypeName_AR = diseaseType.Name_AR,
                    DiseaseName_EN = disease.Name_EN,
                    DiseaseName_AR = disease.Name_AR
                });

            }

            HttpContext.Session.Set("Favorite", model);

            TempData["Success"] = "The Advice has been Added Successfully!";

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return ViewComponent("Favorite");
        }

        public IActionResult RemoveItem(int id)
        {
            TempData["Success"] = "Delete Advice Successfully!";
            FavoritePageVM model = HttpContext.Session.Get<FavoritePageVM>("Favorite");
            model.LstAdvices.Remove(model.LstAdvices.FirstOrDefault(a => a.Id == id));
            HttpContext.Session.Set("Favorite", model);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult RemoveAll()
        {
            TempData["Success"] = "Delete All Advices Successfully!";
            FavoritePageVM model = null;
            HttpContext.Session.Set("Favorite", model);
            return Redirect(Request.Headers["Referer"].ToString());
        }



    }
}
