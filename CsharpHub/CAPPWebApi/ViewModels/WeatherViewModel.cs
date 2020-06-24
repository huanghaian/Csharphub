using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPPWebApi.ViewModels
{
    public class WeatherViewModel
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string Weath { get; set; }
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string WindLevel { get; set; }
        public DateTime Date { get; set; }
    }
}
