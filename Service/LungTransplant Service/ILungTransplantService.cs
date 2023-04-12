using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LungTransplant_Service
{
    public interface ILungTransplantService
    {
        Task<OperationResult> AddRequestAsync(LungTransplantVM model, string userId);
    }
}
