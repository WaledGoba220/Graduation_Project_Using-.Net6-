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
        public IEnumerable<TbAdvice>? LstAdvices { get; set; }
        public AddAdviceVM? AdviceVM { get; set; }
    }
}
