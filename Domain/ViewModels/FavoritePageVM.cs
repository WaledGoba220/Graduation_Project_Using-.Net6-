using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class FavoritePageVM
    {
        public FavoritePageVM()
        {
            LstAdvices = new List<FavoriteItemsVM>();
        }

        public List<FavoriteItemsVM>? LstAdvices { get; set; }
    }
}
