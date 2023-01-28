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
    public class AdviceRepository : BaseRepository<TbAdvice>, IAdviceRepository
    {
        private readonly ApplicationDbContext _context;
        public AdviceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<AdviceVM>> GetMyAdvicesAsync(int doctorId, int pageSize, int ExcludeRecords)
        {
            var items = await _context.TbAdvices
                .Include(a=>a.DiseaseType)
                .Include(a=>a.Disease)
                .Include(a=>a.Doctor)
                .Include(a=>a.AppUser)
                .Include(a=>a.Comments)
                .AsNoTracking()
                .Where(a=>a.DoctorId == doctorId)
                .Select(a => new AdviceVM
                {
                    Id = a.Id,
                    Image = a.Image,
                    Title = a.Title,
                    Content = a.Content,
                    DiseaseTypeName_AR = a.DiseaseType.Name_AR,
                    DiseaseTypeName_EN = a.DiseaseType.Name_EN,
                    DiseaseName_EN = a.Disease.Name_EN,
                    DiseaseName_AR = a.Disease.Name_AR,
                    CreationDateTime = a.CreationDateTime,
                    User = a.AppUser,
                    CommentsCount = a.Comments.AsQueryable().Count()
                })
                .Skip(ExcludeRecords)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }
        public async Task<List<AdviceVM>> GetAdvicesAsync(int pageSize, int ExcludeRecords)
        {
            var items = await _context.TbAdvices
                .Include(a => a.DiseaseType)
                .Include(a => a.Disease)
                .Include(a => a.Doctor)
                .Include(a => a.AppUser)
                .Include(a=>a.Comments)
                .AsNoTracking()
                .Select(a => new AdviceVM
                {
                    Id = a.Id,
                    Image = a.Image,
                    Title = a.Title,
                    Content = a.Content,
                    CreationDateTime = a.CreationDateTime,
                    DiseaseTypeName_AR = a.DiseaseType.Name_AR,
                    DiseaseTypeName_EN = a.DiseaseType.Name_EN,
                    DiseaseName_EN = a.Disease.Name_EN,
                    DiseaseName_AR = a.Disease.Name_AR,
                    User = a.AppUser,
                    CommentsCount = a.Comments.AsQueryable().Count()
                })
                .Skip(ExcludeRecords)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }

        public async Task<List<AdviceVM>> GetAdvicesBySearchFormAsync(SearchAdviceVM searchForm, int pageSize, int ExcludeRecords)
        {
            var items = await _context.TbAdvices
                .Include(a => a.DiseaseType)
                .Include(a => a.Disease)
                .Include(a => a.Doctor)
                .Include(a => a.AppUser)
                .Include(a=>a.Comments)
                .AsNoTracking()
                .Where( a =>
                        a.DiseaseTypeId == searchForm.DiseaseTypeId && 
                        a.DiseaseId == searchForm.DiseaseId &&
                        a.Title.Contains(searchForm.Title))
                .Select(a => new AdviceVM
                {
                    Id = a.Id,
                    Image = a.Image,
                    Title = a.Title,
                    Content = a.Content,
                    CreationDateTime = a.CreationDateTime,
                    DiseaseTypeName_AR = a.DiseaseType.Name_AR,
                    DiseaseTypeName_EN = a.DiseaseType.Name_EN,
                    DiseaseName_EN = a.Disease.Name_EN,
                    DiseaseName_AR = a.Disease.Name_AR,
                    User = a.AppUser,
                    CommentsCount = a.Comments.AsQueryable().Count()
                })
                .Skip(ExcludeRecords)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }

        public async Task<List<AdviceVM>> GetAdvicesByDiseaseTypeAndDisease(int diseaseTypeId, int diseaseId, int pageSize, int ExcludeRecords)
        {
            var items = await _context.TbAdvices
                .Include(a => a.DiseaseType)
                .Include(a => a.Disease)
                .Include(a => a.Doctor)
                .Include(a => a.AppUser)
                .Include(a=>a.Comments)
                .AsNoTracking()
                .Where(a =>
                        a.DiseaseTypeId == diseaseTypeId &&
                        a.DiseaseId == diseaseId)
                .Select(a => new AdviceVM
                {
                    Id = a.Id,
                    Image = a.Image,
                    Title = a.Title,
                    Content = a.Content,
                    CreationDateTime = a.CreationDateTime,
                    DiseaseTypeName_AR = a.DiseaseType.Name_AR,
                    DiseaseTypeName_EN = a.DiseaseType.Name_EN,
                    DiseaseName_EN = a.Disease.Name_EN,
                    DiseaseName_AR = a.Disease.Name_AR,
                    User = a.AppUser,
                    CommentsCount = a.Comments.AsQueryable().Count()
                })
                .Skip(ExcludeRecords)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }

        public async Task<List<AdviceVM>> GetAdvicesByTitle(string title, int pageSize, int ExcludeRecords)
        {
            var items = await _context.TbAdvices
                .Include(a => a.DiseaseType)
                .Include(a => a.Disease)
                .Include(a => a.Doctor)
                .Include(a => a.AppUser)
                .Include(a=>a.Comments)
                .AsNoTracking()
                .Where(a =>
                        a.Title.Contains(title))
                .Select(a => new AdviceVM
                {
                    Id = a.Id,
                    Image = a.Image,
                    Title = a.Title,
                    Content = a.Content,
                    CreationDateTime = a.CreationDateTime,
                    DiseaseTypeName_AR = a.DiseaseType.Name_AR,
                    DiseaseTypeName_EN = a.DiseaseType.Name_EN,
                    DiseaseName_EN = a.Disease.Name_EN,
                    DiseaseName_AR = a.Disease.Name_AR,
                    User = a.AppUser,
                    CommentsCount = a.Comments.AsQueryable().Count()
                })
                .Skip(ExcludeRecords)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }
    }
}
