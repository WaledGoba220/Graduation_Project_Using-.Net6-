using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Graduation_Project.Controllers
{
    [Authorize]
    public class ModelController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public ModelController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        private async Task<string> GetPathFromFile(IFormFile file)
        {
            var _fileName = file.FileName;
            string newName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(_fileName);
            var fileName = string.Concat(newName, extension);
            var root = _env.WebRootPath;
            var path = Path.Combine(root, "Models", fileName);

            using (var fs = System.IO.File.Create(path))
            {
                await file.CopyToAsync(fs);
            }

            return path;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pneumonia(IFormFile file)
        {
            if(file is null)
                return Json(new { success = false, message = "Please Choose Image" });
            
            string path = await GetPathFromFile(file);
            var client = new RestClient("https://goba.onrender.com");
            var request = new RestRequest("/pneumonia/predict")
                .AddParameter("Name", "de.json")
                .AddParameter("Description", "json file")
                .AddFile("image", path);
            var response = await client.PostAsync(request);

            var array = response.Content!.Split('"');

            double result = Convert.ToDouble(array[3]) * 100;

            string message;
            if (result <= 30)
            {
                message = "Normal";
            }
            else if (result < 80 && result > 30)
            {
                string persenatage = string.Format("{0:0.##}", result);
                message = $"{persenatage}% Susceptible to Disease";
            }
            else
            {
                message = "Positive Pneumonia";
            }

            return Json(new { success = true, message = message });
        }

        [HttpPost]
        public async Task<IActionResult> Tuberculosis(IFormFile file)
        {
            if (file is null)
                return Json(new { success = false, message = "Please Choose Image" });

            string path = await GetPathFromFile(file);
            var client = new RestClient("https://goba.onrender.com");
            var request = new RestRequest("/tuberculosis/predict")
                .AddParameter("Name", "de.json")
                .AddParameter("Description", "json file")
                .AddFile("image", path);
            var response = await client.PostAsync(request);

            var array = response.Content!.Split('"');

            double result = Convert.ToDouble(array[3]) * 100;

            string message;
            if (result <= 30)
            {
                message = "Normal";
            }
            else if (result < 80 && result > 30)
            {
                string persenatage = string.Format("{0:0.##}", result);
                message = $"{persenatage}% Susceptible to Disease";
            }
            else
            {
                message = "Positive Tuberculosis";
            }

            return Json(new { success = true, message = message });
        }

    }
}
