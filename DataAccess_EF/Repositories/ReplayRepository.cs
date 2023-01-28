using Domain.Interfaces_Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_EF.Repositories
{
    public class ReplayRepository : BaseRepository<TbReplay>, IReplayRepository
    {
        private readonly ApplicationDbContext _context;
        public ReplayRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
