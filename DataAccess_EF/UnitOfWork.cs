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
        public ICommentRepository TbComments { get; private set; }
        public IReplayRepository TbReplays { get; private set; }
        public IDoctorViewsCountRepository TbDoctorViewsCounts { get; private set; }
        public IRatingRepository TbRatings { get; private set; }
        public IRegistrationRequestsRepository TdRegistrationRequests { get; private set; }
        public IAdviceRequestRepository TbAdviceRequests { get; private set; }
        public IPneumoniaRepository TbPneumonias { get; private set; }
        public ITuberculosisRepository TbTuberculosis { get; private set; }
        public ILungCancerRepository TbLungCancer { get; private set; }


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
            TbComments = new CommentRepository(context);
            TbReplays = new ReplayRepository(context);
            TbDoctorViewsCounts = new DoctorViewsCountRepository(context);
            TbRatings = new RatingRepository(context);
            TdRegistrationRequests = new RegistrationRequestsRepository(context);
            TbAdviceRequests = new AdviceRequestsRepository(context);
            TbPneumonias = new PneumoniaRepository(context);
            TbTuberculosis = new TuberculosisRepository(context);
            TbLungCancer = new LungCancerRepository(context);
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
