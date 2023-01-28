using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class AddAdviceVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Your Title")]
        [MaxLength(100 ,ErrorMessage = "The Title must be less than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter Your Content")]
        [MinLength(200, ErrorMessage = "Content must be written more than 200 characters")]
        public string Content { get; set; }
        public DateTime? CreationDateTime { get; set; } = DateTime.UtcNow;

        // Forign Key
        [Required(ErrorMessage = "Please Choose Your Type Of Disease")]
        public int DiseaseTypeId { get; set; }
        public int DiseaseId { get; set; }
        public int DoctorId { get; set; }
        public string UserId { get; set; }

    }
}
