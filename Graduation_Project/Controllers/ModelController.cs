using Domain;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Graduation_Project.Controllers
{
    [Authorize]
    public class ModelController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private const string BaseUrl = "https://goba.onrender.com"; 
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
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/pneumonia/predict")
                .AddParameter("Name", "de.json")
                .AddParameter("Description", "json file")
                .AddFile("image", path);
            RestResponse response = await client.PostAsync(request);

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
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/tuberculosis/predict")
                .AddParameter("Name", "de.json")
                .AddParameter("Description", "json file")
                .AddFile("image", path);
            RestResponse response = await client.PostAsync(request);

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

        [HttpPost]
        public async Task<IActionResult> LungCancer(LungCancerVM model)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("Cancer/predict/", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                Age = model.Age,
                Gender = model.Gender,
                AirPollution = model.AirPollution,
                Alcoholuse= model.Alcoholuse,
                DustAllergy= model.DustAllergy,
                OccuPationalHazards= model.OccuPationalHazards,
                GeneticRisk= model.GeneticRisk,
                chronicLungDisease= model.ChronicLungDisease,
                BalancedDiet= model.BalancedDiet,
                Obesity= model.Obesity,
                Smoking= model.Smoking,
                PassiveSmoker= model.PassiveSmoker,
                ChestPain= model.ChestPain,
                CoughingofBlood= model.CoughingofBlood,
                Fatigue= model.Fatigue,
                WeightLoss= model.WeightLoss,
                ShortnessofBreath= model.ShortnessofBreath,
                Wheezing= model.Wheezing,
                SwallowingDifficulty= model.SwallowingDifficulty,
                ClubbingofFingerNail= model.ClubbingofFingerNail,
                FrequentCold= model.FrequentCold,
                DryCough= model.DryCough,
                Snoring= model.Snoring
            };

            var bodyy = JsonConvert.SerializeObject(body);
            request.AddBody(bodyy, "application/json");

            RestResponse response = await client.ExecuteAsync(request);
            byte result = Convert.ToByte(response.Content!.Substring(14, 1));

            string message;
            if (result == 0)
                message = "Normal";
            else
                message = "Positive Lung Cancer";

            return Json(new { success = true, message = message });
        }

    }
}
