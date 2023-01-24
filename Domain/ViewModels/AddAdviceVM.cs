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
        public DateTime? CreationDateTime { get; set; } = DateTime.Now;

        // Forign Key
        [Required(ErrorMessage = "Please Choose Your Type Of Disease")]
        [ForeignKey("DiseaseType")]
        public int DiseaseTypeId { get; set; }
        [ForeignKey("Disease")]
        public int? DiseaseId { get; set; }
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }

    }
}
