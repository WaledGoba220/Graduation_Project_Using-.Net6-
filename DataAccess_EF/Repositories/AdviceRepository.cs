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
                .AsNoTracking()
                .Where(a=>a.DoctorId == doctorId)
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
                    DiseaseName_AR = a.Disease.Name_AR

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
                    DiseaseName_AR = a.Disease.Name_AR

                })
                .Skip(ExcludeRecords)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }

        public async Task<List<AdviceVM>> GetAdvicesBySearchFormAsync(SearchAdviceVM searchForm, int pageSize, int ExcludeRecords)
        {
            if(searchForm.DiseaseTypeId != 0 && searchForm.DiseaseId != 0 && !String.IsNullOrEmpty(searchForm.Title))
            {
                var items = await _context.TbAdvices
                    .Include(a => a.DiseaseType)
                    .Include(a => a.Disease)
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
                        DiseaseName_AR = a.Disease.Name_AR

                    })
                    .Skip(ExcludeRecords)
                    .Take(pageSize)
                    .ToListAsync();

                return items;
            }

            else if (searchForm.DiseaseTypeId !=0 && searchForm.DiseaseId != 0)
            {
                var items = await _context.TbAdvices
                    .Include(a => a.DiseaseType)
                    .Include(a => a.Disease)
                    .AsNoTracking()
                    .Where(a =>
                            a.DiseaseTypeId == searchForm.DiseaseTypeId &&
                            a.DiseaseId == searchForm.DiseaseId)
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
                        DiseaseName_AR = a.Disease.Name_AR

                    })
                    .Skip(ExcludeRecords)
                    .Take(pageSize)
                    .ToListAsync();

                return items;
            }

            else 
            {
                var items = await _context.TbAdvices
                    .Include(a => a.DiseaseType)
                    .Include(a => a.Disease)
                    .AsNoTracking()
                    .Where(a =>
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
                        DiseaseName_AR = a.Disease.Name_AR

                    })
                    .Skip(ExcludeRecords)
                    .Take(pageSize)
                    .ToListAsync();

                return items;
            }
        }
    }
}
