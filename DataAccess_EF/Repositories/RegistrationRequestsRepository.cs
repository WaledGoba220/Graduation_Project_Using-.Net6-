using Domain.Interfaces_Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_EF.Repositories
{
    public class RegistrationRequestsRepository : BaseRepository<TdRegistrationRequests>, IRegistrationRequestsRepository
    {
        private readonly ApplicationDbContext _context;
        public RegistrationRequestsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<TdRegistrationRequests> GetLatestWeek()
        {
            IQueryable<TdRegistrationRequests> result =
                _context.TdRegistrationRequests
                .OrderByDescending(a => a.Date)
                .Take(7);

            return result;
        }
    }
}
