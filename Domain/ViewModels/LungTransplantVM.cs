using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels
{
    public class LungTransplantVM
    {
        [Required(ErrorMessage = "Please Enter your Name")]
        [MinLength(10, ErrorMessage = "Enter Your Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter your Age")]
        [Range(1.0, 100, ErrorMessage = "Enter Valied Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please Enter your Phone")]
        [Range(3.0, 9999999999, ErrorMessage = "Enter Valied Phone")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Please Enter your National ID")]
        public long NationalId { get; set; }

        [Required(ErrorMessage = "Please Choose your City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter your Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Upload National Image")]
        public IFormFile NationalImage { get; set; }

        [Required(ErrorMessage = "Please Upload Analysis File")]
        public IFormFile AnalysisFile { get; set; }

        [Required(ErrorMessage = "Please Upload Chest Ray Image")]
        public IFormFile ChestRayImage { get; set; }
    }
}
