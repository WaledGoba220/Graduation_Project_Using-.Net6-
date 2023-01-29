using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class FavoriteItemsVM
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string DiseaseTypeName_EN { get; set; }
        public string DiseaseTypeName_AR { get; set; }
        public string DiseaseName_EN { get; set; }
        public string DiseaseName_AR { get; set; }
        public string UserId { get; set; }
        public int CommentsCount { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
