using Domain.Models;
using Domain.ViewModels;
using E_Exam.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserService
    {
        Task Initialize();
        Task<OperationResult> RegisterAsync(ApplicationUser user, string password);
        Task<OperationResult> LoginAsync(LoginVM model);
        Task<OperationResult> ConfirmEmailAsync(ConfirmEmailVM model);
        Task<OperationResult> UpdateUserPhotoAsync(ApplicationUser user, IFormFile myFile);
        Task<OperationResult> UpdateUserInfoAsync(string userName, string fullName, ApplicationUser user);
        IQueryable<UsersPageVM> GetAllUsers();
        IQueryable<UsersPageVM> GetBlockedUsers();
        Task<int> UserRegistrationCount();
        Task<OperationResult> ToggleBlockUserAsync(string userId);
        Task<OperationResult> DeleteUserAsync(string userId);
        Task<int> SaveTotlaRegistrationsEveryDay();
    }
}
