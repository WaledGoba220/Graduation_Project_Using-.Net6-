using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class AdvicesVM
    {
        public List<AdviceVM>? LstAdvices { get; set; }
        public PagePagination? Pagination { get; set; }
        public SearchAdviceVM? SearchAdvice { get; set; } = new();
    }
}
