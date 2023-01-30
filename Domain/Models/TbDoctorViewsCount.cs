using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbDoctorViewsCount
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int Count { get; set; }

        public TbDoctor? Doctor { get; set; }
    }
}
