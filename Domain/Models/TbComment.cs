using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class TbComment
    {
        public TbComment()
        {
            Replays = new HashSet<TbReplay>();
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime? CreationDateTime { get; set; } = DateTime.UtcNow;

        // Forign Key
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        [ForeignKey("Advice")]
        public int AdviceId { get; set; }


        // Relations
        public ApplicationUser? AppUser { get; set; }
        public TbAdvice? Advice { get; set; }
        public virtual ICollection<TbReplay>? Replays { get; set; }
    }
}
