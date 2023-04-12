using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbLungTransplant
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Phone { get; set; }
        public long NationalId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public byte[] NationalImage { get; set; }
        public byte[] AnalysisFile { get; set; }
        public byte[] ChestRayImage { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public string? UserId { get; set; }

        // Relations
        public ApplicationUser? User { get; set; }
    }
}
