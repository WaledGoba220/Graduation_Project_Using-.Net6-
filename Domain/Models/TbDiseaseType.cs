using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbDiseaseType
    {
        public TbDiseaseType()
        {
            Diseases = new HashSet<TbDisease>();
            Advices = new HashSet<TbAdvice>();
        }
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_AR { get; set; }


        // Relations
        public virtual ICollection<TbDisease>? Diseases { get; set; }
        public virtual ICollection<TbAdvice>? Advices { get; set; }

    }
}
