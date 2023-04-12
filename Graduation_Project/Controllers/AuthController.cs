using Domain.Models;
using Domain.Services;
using Domain.ViewModels;
using E_Exam.Utility.EmailSender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IUserService userService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginVM model)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                // Check User Validation
                var result = await _userService.LoginAsync(model);

                if (result.Success)
                {
                    // Get User From Db
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    
                    string role = string.Empty;
                    if(userRoles.Count > 0)
                        role = userRoles[0];
                    else
                        role = "Patient";
                    
                    response.IsSuccess = true;
                    response.Message = result.Message;
                    response.Content = new
                    {
                        UserId = user.Id,
                        Role = role,
                    };
                    return Ok(response);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = result.Message;
                    return Unauthorized(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
