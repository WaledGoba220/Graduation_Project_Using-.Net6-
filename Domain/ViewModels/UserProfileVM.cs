using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class UserProfileVM
    {
        public ApplicationUser? User { get; set; }
        public TbDoctor? Doctor { get; set; }
        public TbDoctorViewsCount? DoctorViewsCount { get; set; }
        public List<AdviceVM>? LstAdvices { get; set; } = new List<AdviceVM>();
    }
}
