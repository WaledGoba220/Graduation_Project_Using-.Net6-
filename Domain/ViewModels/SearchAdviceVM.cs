using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class SearchAdviceVM
    {
        public string? Title { get; set; }
        public int DiseaseTypeId { get; set; }
        public int DiseaseId { get; set; }

        // Relations
        public IEnumerable<TbDisease>? Diseases { get; set; } = new List<TbDisease>();
    }
}
