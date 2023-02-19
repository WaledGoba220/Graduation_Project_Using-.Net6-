using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TbLungCancer
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public int AirPollution { get; set; }
        public int Alcoholuse { get; set; }
        public int DustAllergy { get; set; }
        public int OccuPationalHazards { get; set; }
        public int GeneticRisk { get; set; }
        public int ChronicLungDisease { get; set; }
        public int BalancedDiet { get; set; }
        public int Obesity { get; set; }
        public int Smoking { get; set; }
        public int PassiveSmoker { get; set; }
        public int ChestPain { get; set; }
        public int CoughingofBlood { get; set; }
        public int Fatigue { get; set; }
        public int WeightLoss { get; set; }
        public int ShortnessofBreath { get; set; }
        public int Wheezing { get; set; }
        public int SwallowingDifficulty { get; set; }
        public int ClubbingofFingerNail { get; set; }
        public int FrequentCold { get; set; }
        public int DryCough { get; set; }
        public int Snoring { get; set; }
        public string Status { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        // Relations
        public TbDoctor? Doctor { get; set; }
    }
}
