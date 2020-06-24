using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAPPWebApi.Models
{
    public class TodayWeather
    {
        [Key]
        [DisplayName("Id")]
        public string Id { get; set; }
        [DisplayName("白天/晚间")]

        public string Day { get; set; }
        [DisplayName("天气")]

        public string Weather { get; set;}
        [DisplayName("温度")]

        public string Temperature { get; set; }
        [DisplayName("风向")]

        public string Wind { get; set; }
        [DisplayName("天空")]
        public DayType DayType { get; set; }
        public string Sky { get; set; }
        public DateTime Today { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsOverTime { get; set; }

    }
    public enum DayType
    {
        CurrentDay,
        CurrentNight,
        NewCurrentDay
    }
}
