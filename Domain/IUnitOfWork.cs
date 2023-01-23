using Domain.Interfaces;
using Domain.Interfaces_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IContactRepository TbContacts { get; }
        ISpecializationRepository TbSpecialization { get; }
        IDoctorRepository TbDoctors { get; }
        IClinicImageRepository TbClinicImages { get; }
        IDiseaseTypeRepository TbDiseaseTypes { get; }
        IDiseaseRepository TbDiseases { get; }
        IAdviceRepository TbAdvices { get; }


        Task<int> Complete();
    }
}
