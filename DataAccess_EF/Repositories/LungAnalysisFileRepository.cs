using Domain.Interfaces_Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_EF.Repositories
{
    public class LungAnalysisFileRepository : BaseRepository<TbLungAnalysisFile>, ILungAnalysisFileRepository
    {
        private readonly ApplicationDbContext _context;
        public LungAnalysisFileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
