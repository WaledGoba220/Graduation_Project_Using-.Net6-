using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class AdviceDetailsVM
    {
        public TbAdvice? Advice { get; set; }
        public TbComment? Comment { get; set; }
        public TbReplay? Replay { get; set; }
        public IEnumerable<TbReplay>? LstReplays { get; set; }
        public IEnumerable<TbComment>? LstComments { get; set; }
        public List<AdviceVM>? LatestAdvices { get; set; }
    }
}
