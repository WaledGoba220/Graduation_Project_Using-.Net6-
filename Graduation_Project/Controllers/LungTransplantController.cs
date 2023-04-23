using Domain;
using Domain.Models;
using Domain.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Service.LungTransplant_Service;
using Utility.Consts;

namespace Graduation_Project.Controllers
{
    [Authorize]
    public class LungTransplantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ILungTransplantService _lungTransplantService;
        private IUnitOfWork _unitOfWork; 
        private readonly IWebHostEnvironment _env;
        public LungTransplantController(UserManager<ApplicationUser> userManager, 
            IUnitOfWork unitOfWork, ILungTransplantService lungTransplantService,
             IWebHostEnvironment env)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _lungTransplantService = lungTransplantService;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RequestDetails(int id)
        {
            var currentUser = await GetCurrentUser();
            var item = await _unitOfWork.TbLungTransplant.GetFirstOrDefaultAsync(a => a.Id == id && a.UserId == currentUser.Id);
            if (item is not null)
            {
                return View(item);
            }

            TempData["Error"] = "Not found request with ID: " + id + "";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest(LungTransplantVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();
                return View("Index", model);
            }

            var currentUser = await GetCurrentUser();
            var result = await _lungTransplantService.AddRequestAsync(model, currentUser.Id);
            if (result.Success)
            {
                // Upload AnalysisFile
                var uploadAnalysisFile = await UploadAnalysisFileAsync(model.AnalysisFile, int.Parse(result.Message));

                // Get Image Path Name For Chest Ray Image
                var chestRayImageName = await GetPathAndNameFromFile(model.ChestRayImage);
                string chestRayPath = Path.Combine(_env.WebRootPath, "Models", chestRayImageName);

                // Hangfire
                BackgroundJob.Schedule(() => CheckPositivePneumonia(chestRayPath, result.Message), TimeSpan.FromMinutes(5));

                TempData["Success"] = "Send your Request Successfully!";
                return View("Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return View("Index", model);
            }
        }

        public async Task<IActionResult> Download(int id)
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


        public async Task<string> CheckPositivePneumonia(string chestRayPath, string id)
        {
            try
            {
                var client = new RestClient(ModeBaseUrl.BaseUrl);
                var request = new RestRequest("/pneumonia/predict")
                    .AddParameter("Name", "de.json")
                    .AddParameter("Description", "json file")
                    .AddFile("image", chestRayPath);
                RestResponse response = await client.PostAsync(request);

                var array = response.Content!.Split('"');

                double persentage = 0;
                double.TryParse(array[3], System.Globalization.NumberStyles.Number, new System.Globalization.CultureInfo("en-US"), out persentage);
                double result = persentage * 100;

                var getRequestById = await _unitOfWork.TbLungTransplant.GetFirstOrDefaultAsync(a => a.Id == int.Parse(id));
                getRequestById.Status = Status.Processing;
                
                if (result < 80)
                {
                    getRequestById.Status = Status.Rejected;
                }

                _unitOfWork.TbLungTransplant.Update(getRequestById);
                await _unitOfWork.Complete();

                return getRequestById.Status;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private async Task<bool> UploadAnalysisFileAsync(IFormFile file, int lungId)
        {
            var _fileName = file.FileName;
            string newName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(_fileName);
            var fileName = string.Concat(newName, extension);
            var root = _env.WebRootPath;
            var path = Path.Combine(root, "Uploads", fileName);

            using (var fs = System.IO.File.Create(path))
            {
                await file.CopyToAsync(fs);
            }

            TbLungAnalysisFile dt = new()
            {
                OriginalFileName = _fileName,
                FileName = fileName,
                ContentType = file.ContentType,
                Size = file.Length,
                LungTransplantId = lungId
            };

            await _unitOfWork.TbLungAnalysisFile.AddAsync(dt);
            await _unitOfWork.Complete();

            return true;
        }
        private async Task<string> GetPathAndNameFromFile(IFormFile file)
        {
            var _fileName = file.FileName;
            string newName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(_fileName);
            var imageName = string.Concat(newName, extension);
            var root = _env.WebRootPath;
            var path = Path.Combine(root, "Models", imageName);

            using (var fs = System.IO.File.Create(path))
            {
                await file.CopyToAsync(fs);
            }

            return imageName;
        }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
