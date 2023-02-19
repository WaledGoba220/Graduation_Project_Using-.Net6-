using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Utility.Consts;

namespace Graduation_Project.Controllers
{
    [Authorize(Roles = Roles.Doctor)]
    public class ModelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private IUnitOfWork _unitOfWork;
        private const string BaseUrl = "https://goba.onrender.com"; 
        public ModelController(IUnitOfWork unitOfWork, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
        private async Task<List<string>> GetPathAndNameFromFile(IFormFile file)
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

            var list = new List<string>();
            list.Add(fileName);
            list.Add(path);

            return list;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pneumonia(ModelFileVm model)
        {
            if(model.File is null)
                return Json(new { success = false, message = "Please Choose Image" });
            
            List<string> list = await GetPathAndNameFromFile(model.File);

            string path = list[1];
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/pneumonia/predict")
                .AddParameter("Name", "de.json")
                .AddParameter("Description", "json file")
                .AddFile("image", path);
            RestResponse response = await client.PostAsync(request);

            var array = response.Content!.Split('"');

            double result = Convert.ToDouble(array[3]) * 100;

            string message = String.Empty;
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

            // Save Data
            TbPneumonia tbPneumonia = new()
            {
                ImageName = list[0],
                PatientName = model.PatientName,
                Status = message,
            };
            await SavePneumonia(tbPneumonia);

            return Json(new { success = true, message = message });
        }

        public async Task<IActionResult> SavePneumonia(TbPneumonia model)
        {
            var currentUser = await GetCurrentUser();
            model.DoctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);

            await _unitOfWork.TbPneumonias.AddAsync(model);
            await _unitOfWork.Complete();

            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> Tuberculosis(ModelFileVm model)
        {
            if (model.File is null)
                return Json(new { success = false, message = "Please Choose Image" });

            List<string> list = await GetPathAndNameFromFile(model.File);

            string path = list[1];
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/tuberculosis/predict")
                .AddParameter("Name", "de.json")
                .AddParameter("Description", "json file")
                .AddFile("image", path);
            RestResponse response = await client.PostAsync(request);

            var array = response.Content!.Split('"');

            double result = Convert.ToDouble(array[3]) * 100;

            string message = String.Empty;
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

            // Save Data
            TbTuberculosis tbTuberculosis = new()
            {
                ImageName = list[0],
                PatientName = model.PatientName,
                Status = message,
            };
            await SaveTuberculosis(tbTuberculosis);

            return Json(new { success = true, message = message });
        }

        public async Task<IActionResult> SaveTuberculosis(TbTuberculosis model)
        {
            var currentUser = await GetCurrentUser();
            model.DoctorId = await _unitOfWork.TbDoctors.GetIdByUserIdAsync(currentUser.Id);

            await _unitOfWork.TbTuberculosis.AddAsync(model);
            await _unitOfWork.Complete();

            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> LungCancer(TbLungCancer model)
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
