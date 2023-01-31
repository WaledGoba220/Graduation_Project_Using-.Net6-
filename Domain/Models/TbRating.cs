using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbRating
    {
        public int Id { get; set; }
        public byte Rate { get; set; }

        // Forign Key
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Relations
        public TbDoctor? Doctor { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
