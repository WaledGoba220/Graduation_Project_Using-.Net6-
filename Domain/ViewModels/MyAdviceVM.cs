using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class MyAdviceVM
    {
        public List<AdviceVM>? LstAdvices { get; set; }
        public AddAdviceVM? AdviceVM { get; set; }
        public PagePagination? Pagination { get; set; }
    }
}
