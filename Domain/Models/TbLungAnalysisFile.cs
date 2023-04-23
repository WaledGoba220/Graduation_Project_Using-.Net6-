using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbLungAnalysisFile
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        // Forign Key
        [ForeignKey("LungTransplant")]
        public int LungTransplantId { get; set; }

        // Relations
        public TbLungTransplant? LungTransplant { get; set; }
    }
}
