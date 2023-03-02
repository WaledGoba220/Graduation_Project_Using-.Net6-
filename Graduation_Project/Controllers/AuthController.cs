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
        public async Task<IActionResult> LoginAsync([FromForm] LoginVM model)
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
                    {
                        role = userRoles[0];
                    }
                    else
                    {
                        role = "Patient";
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Audience = _configuration["JWT:Audience"],
                        Issuer = _configuration["JWT:Issuer"],
                        Expires = DateTime.UtcNow.AddDays(5),
                        Subject = new ClaimsIdentity(claims),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    response.IsSuccess = true;
                    response.Message = result.Message;
                    response.Content = new
                    {
                        UserId = user.Id,
                        Role = role,
                        Token = tokenHandler.WriteToken(token),
                        ExpireDate = DateTime.UtcNow.AddDays(5).ToString("MM/dd/yyyy HH:mm:ss")
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
                return BadRequest(ex.Message);
            }
        }



    }
}
