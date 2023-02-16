using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbPneumonia
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public string UserId { get; set; }

        // Relations
        public ApplicationUser? User { get; set; }
    }
}
