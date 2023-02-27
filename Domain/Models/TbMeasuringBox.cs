using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbMeasuringBox
    {
        public int Id { get; set; }
        public float? Temperature { get; set; }
        public float? Oxygen { get; set; }
        public float? HeartRate { get; set; }
        public string PatientName { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        // Forign Key
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Relations
        public ApplicationUser? User { get; set; }

    }
}
