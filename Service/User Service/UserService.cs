using Utility.Consts;
using Domain.Models;
using Domain.ViewModels;
using E_Exam.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public async Task Initialize()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(Roles.Doctor))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Doctor));
            }

            var adminEmail = "admin@admin.com";
            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                ApplicationUser user = new()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, "Abdalla0159357");
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            }
        }

        public async Task<OperationResult> RegisterAsync(ApplicationUser user, string password)
        {
            try
            {
                var createUser = await _userManager.CreateAsync(user, password);
                if (createUser.Succeeded)
                {
                    return OperationResult.Succeeded("Register Successfully!, Please Confirm your Email");
                }
                else
                {
                    var errors = createUser.Errors.FirstOrDefault().Description;
                    return OperationResult.Error(errors);
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public async Task<OperationResult> LoginAsync(LoginVM model)
        {
            try
            {
                var existedUser = await _userManager.FindByEmailAsync(model.Email);
                if (existedUser == null)
                {
                    return OperationResult.Error("Invalid Email");
                }
                else if (existedUser.LockoutEnabled == false)
                {
                    return OperationResult.Error("This Account Has been Blocked");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(existedUser.UserName, model.Password, true, true);
                    if (result.Succeeded)
                    {
                        return OperationResult.Succeeded("Login Successfully!");
                    }
                    else if (result.IsNotAllowed)
                    {
                        return OperationResult.Error("You must Confirm the email, To be able to login");
                    }
                    else
                    {
                        return OperationResult.Error("Invalid Password");
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }


        }

        public async Task<OperationResult> ConfirmEmailAsync(ConfirmEmailVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is not null)
            {
                if (!user.EmailConfirmed)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, model.Token);
                    if (result.Succeeded)
                    {
                        return OperationResult.Succeeded("Your Email confirmed Successfully!");
                    }
                    else
                    {
                        var errors = result.Errors.FirstOrDefault().Description;
                        return OperationResult.Error(errors);
                    }
                }
                else
                {
                    return OperationResult.Error("Your Email already confirmed");
                }
            }
            else
            {
                return OperationResult.Error($"Notfound User With ID: {model.UserId}");
            }
        }

        public async Task<OperationResult> UpdateUserPhotoAsync(ApplicationUser user, IFormFile myFile)
        {
            try
            {
                // Convert Photo from IFormFile to byte[]
                using var dataStream = new MemoryStream();
                await myFile.CopyToAsync(dataStream);

                user.Photo = dataStream.ToArray();
                await _userManager.UpdateAsync(user);

                return OperationResult.Succeeded("Update Photo Successfully!");
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

        public async Task<OperationResult> UpdateUserInfoAsync(string userName, string fullName, ApplicationUser user)
        {
            try
            {
                var findUserByUsername = await _userManager.FindByNameAsync(userName);
                if(findUserByUsername != null && user.UserName != userName)
                {
                    return OperationResult.Error("This Username already Existing");
                }
                else
                {
                    user.UserName = userName;
                    if (!string.IsNullOrEmpty(fullName))
                    {
                        user.FullName = fullName;
                    }
                    
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                        return OperationResult.Succeeded("Update Information Successfully!");
                    else
                        return OperationResult.Error(result.Errors.FirstOrDefault()!.Description);
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
        }

    }
}
