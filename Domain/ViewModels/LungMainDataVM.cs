﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class LungMainDataVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public byte[] ChestRayImage { get; set; }
        public byte[] NationalImage { get; set; }
        public int Age { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
