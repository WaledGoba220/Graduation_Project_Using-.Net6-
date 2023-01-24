using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces_Repository
{
    public interface IAdviceRepository : IBaseRepository<TbAdvice>
    {
        Task<List<AdviceVM>> GetMyAdvicesAsync(int doctorId, int pageSize, int ExcludeRecords);

        Task<List<AdviceVM>> GetAdvicesAsync(int pageSize, int ExcludeRecords);
        Task<List<AdviceVM>> GetAdvicesBySearchFormAsync(SearchAdviceVM searchForm, int pageSize, int ExcludeRecords);
    }
}
