using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class MeasuringBoxVM
    {
        public float? Temperature { get; set; }
        public float? Oxygen { get; set; }
        public float? HeartRate { get; set; }
        public string PatientName { get; set; }
        public string UserId { get; set; }
    }
}
