using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Advice_Service
{
    public interface IAdviceService
    {
        Task<OperationResult> AddAsync(AddAdviceVM model, IFormFile file);
        Task<OperationResult> UpdateAsync(AddAdviceVM model, IFormFile file);
    }
}
