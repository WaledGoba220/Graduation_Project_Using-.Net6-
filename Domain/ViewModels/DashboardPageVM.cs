using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class DashboardPageVM
    {
        public RegistrationRequestsVM? RegistrationsRequests { get; set; }
        public AdviceRequestsVM? AdvicesRequests { get; set; }
    }
}
