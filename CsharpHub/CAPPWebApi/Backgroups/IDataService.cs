using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPPWebApi.Backgroups
{
    public interface IDataService
    {
        Task RunTask();
        Task UpdateWheatherInNight();
    }
}
