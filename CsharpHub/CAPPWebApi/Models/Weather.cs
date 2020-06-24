﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPPWebApi.Models
{
    public class Weather
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string Weath { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string WindLevel { get; set; }
        public DateTime UpdateTime { get; set; }
        //public bool IsOverTime { get; set; }
    }
}
