using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbAdvice
    {
        public TbAdvice()
        {
            Comments = new HashSet<TbComment>();
        }

        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        // Forign Key
        [ForeignKey("DiseaseType")]
        public int DiseaseTypeId { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }

        // Relations
        public TbDiseaseType? DiseaseType { get; set; }
        public TbDisease? Disease { get; set; }
        public TbDoctor? Doctor { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public virtual ICollection<TbComment>? Comments { get; set; }
    }
}
