using Domain.Interfaces_Repository;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_EF.Repositories
{
    public class AdviceRequestsRepository : BaseRepository<TbAdviceRequest>, IAdviceRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public AdviceRequestsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<int> SaveTotalAdvicesEveryDay()
        {
            int count = await _context.TbAdvices.AsNoTracking().AsQueryable().CountAsync();

            TbAdviceRequest model = new()
            {
                TotalAdvices = count
            };

            await _context.TbAdviceRequests.AddAsync(model);
            _context.SaveChanges();

            return count;
        }
        
        public IQueryable<TbAdviceRequest> GetLatestWeek()
        {
            IQueryable<TbAdviceRequest> result =
                _context.TbAdviceRequests
                .OrderByDescending(a => a.Date)
                .Take(7);

            return result;
        }
        
        public async Task<AdviceRequestsVM> AdviceRequestsAsync()
        {
            var adviceRequests = GetLatestWeek().ToArray();

            AdviceRequestsVM model = new();
            model.AdvicesCountArray = new int[7];
            for (int i = 0; i < 7; i++)
            {
                model.AdvicesCountArray[i] = adviceRequests[6 - i].TotalAdvices;
            }

            double average = ((double)(model.AdvicesCountArray[6] / (double)model.AdvicesCountArray[0]) * 100 - 100);
            if (average == 1)
                model.Average = 0;
            else
                model.Average = average;

            int advicesCount = await _context.TbAdvices.AsNoTracking().AsQueryable().CountAsync();

            model.TotalAdvices = advicesCount;

            return model;
        }


    }
}
