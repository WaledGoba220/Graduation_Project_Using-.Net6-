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
    public class LungTransplantRepository : BaseRepository<TbLungTransplant>, ILungTransplantRepository
    {
        private readonly ApplicationDbContext _context;
        public LungTransplantRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<LungMainDataVM>> GetMainDataAsync()
        {
            var items = await _context.TbLungTransplants
                .AsNoTracking()
                .Select(a => new LungMainDataVM
                {
                    Id = a.Id,
                    Status = a.Status,
                    Age = a.Age,
                    CreationDateTime = a.CreationDateTime,
                    ChestRayImage = a.ChestRayImage,
                    NationalImage = a.NationalImage,
                    FullName = a.FullName
                })
                .OrderByDescending(a => a.CreationDateTime)
                .ToListAsync();

            return items;
        }
    }
}
