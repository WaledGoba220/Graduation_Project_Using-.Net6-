using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Domain.Interfaces_Repository
{
    public interface IRegistrationRequestsRepository : IBaseRepository<TdRegistrationRequests>
    {
        IQueryable<TdRegistrationRequests> GetLatestWeek();
        Task<RegistrationRequestsVM> RegistrationRequestsAsync();
    }
}
