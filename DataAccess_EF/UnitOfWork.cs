using DataAccess_EF.Repositories;
using Domain;
using Domain.Interfaces;
using Domain.Interfaces_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IContactRepository TbContacts { get; private set; }
        public ISpecializationRepository TbSpecialization { get; private set; }
        public IDoctorRepository TbDoctors { get; private set; }
        public IClinicImageRepository TbClinicImages { get; private set; }
        public IDiseaseTypeRepository TbDiseaseTypes { get; private set; }
        public IDiseaseRepository TbDiseases { get; private set; }
        public IAdviceRepository TbAdvices { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            TbContacts = new ContactRepository(context);
            TbSpecialization = new SpecializationRepository(context);
            TbDoctors = new DoctorRepository(context);
            TbClinicImages = new ClinicImageRepository(context);
            TbDiseaseTypes = new DiseaseTypeRepository(context);
            TbDiseases = new DiseaseRepository(context);
            TbAdvices = new AdviceRepository(context);
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
