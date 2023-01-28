using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbReplay
    {
        public int Id { get; set; }
        public string Replay { get; set; }
        public DateTime? CreationDateTime { get; set; } = DateTime.UtcNow;

        // Forign Key
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        [ForeignKey("Advice")]
        public int AdviceId { get; set; }


        // Relations
        public TbComment? Comment { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public TbAdvice? Advice { get; set; }
    }
}
