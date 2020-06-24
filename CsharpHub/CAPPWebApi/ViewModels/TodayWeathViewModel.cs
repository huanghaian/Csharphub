using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CAPPWebApi.ViewModels
{
    public class TodayWeathViewModel
    {
        [DisplayName("白天/晚间")]

        public string Day { get; set; }
        [DisplayName("天气")]

        public string Weather { get; set; }
        [DisplayName("温度")]

        public string Temperature { get; set; }
        [DisplayName("风向")]

        public string Wind { get; set; }
        [DisplayName("天空")]

        public string Sky { get; set; }
        [DisplayName("当天")]

        public DateTime Today { get; set; }
    }
}
