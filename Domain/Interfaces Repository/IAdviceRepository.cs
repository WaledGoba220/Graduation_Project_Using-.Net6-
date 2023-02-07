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
        Task<List<AdviceVM>> GetAllAdvicesAsync();
        Task<List<AdviceVM>> GetMyAdvicesAsync(int doctorId, int pageSize, int ExcludeRecords);
        Task<List<AdviceVM>> GetAdvicesAsync(int pageSize, int ExcludeRecords);
        Task<List<AdviceVM>> GetAdvicesBySearchFormAsync(SearchAdviceVM searchForm, int pageSize, int ExcludeRecords);
        Task<List<AdviceVM>> GetAdvicesByDiseaseTypeAndDisease(int diseaseTypeId, int diseaseId, int pageSize, int ExcludeRecords);
        Task<List<AdviceVM>> GetAdvicesByTitle(string title, int pageSize, int ExcludeRecords);
        Task<List<AdviceVM>> GetAdvicesByDocitorIdAsync(int doctorId);
        Task<List<AdviceVM>> GetLatestAdvicesByDoctorId(int doctorId);
        Task<List<AdviceVM>> GetLatestAdvices();
    }
}
