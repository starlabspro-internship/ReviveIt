﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TechnicianViewModel
    {
        public string FullName { get; set; }
        public string Expertise { get; set; }
        public int? Experience { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Review { get; set; }
        public double Rating { get; set; }
    }
}

