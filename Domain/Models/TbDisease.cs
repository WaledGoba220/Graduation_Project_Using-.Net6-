using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbDisease
    {
        public TbDisease()
        {
            Advices = new HashSet<TbAdvice>();
        }

        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_AR { get; set; }


        // Forign Key
        public int DiseaseTypeId { get; set; }

        // Relations
        public TbDiseaseType? DiseaseType { get; set; }
        public virtual ICollection<TbAdvice>? Advices { get; set; }
    }
}
