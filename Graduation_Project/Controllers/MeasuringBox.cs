using Domain;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeasuringBox : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;
        public MeasuringBox(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync([FromBody] MeasuringBoxVM model)
        {
            BaseResponse response = new();

            if (String.IsNullOrEmpty(model.PatientName))
            {
                response.IsSuccess = false;
                response.Message = "Patient Name is Required";
                return BadRequest(response);
            }

            var findUserById = await _userManager.FindByIdAsync(model.UserId);
            if(findUserById is not null)
            {
                try
                {
                    TbMeasuringBox db = new()
                    {
                        Temperature = model.Temperature,
                        Oxygen = model.Oxygen,
                        HeartRate = model.HeartRate,
                        PatientName = model.PatientName,
                        UserId = model.UserId
                    };

                    await _unitOfWork.TbMeasuringBox.AddAsync(db);
                    await _unitOfWork.Complete();

                    response.IsSuccess = true;
                    response.Message = "Save Data Successfully";
                    return Ok(response);
                
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                    return BadRequest(response);
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = $"Not Found User With ID: {model.UserId}";
                return BadRequest(response);
            }
        }


    }
}
