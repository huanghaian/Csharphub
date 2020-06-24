using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPPWebApi.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CAPPWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly MyDbContext _context;
        public WeatherController(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public async Task<WeatherViewModel[]> Get()
        {
            var retults = await _context.Weathers.Where(t => t.UpdateTime.Date == DateTime.Now.Date).Select(t => new WeatherViewModel
            {
                Id = t.Id,
                Temperature = t.Temperature,
                Day = t.Day,
                Weath = t.Weath,
                Wind = t.Wind,
                WindLevel = t.WindLevel,
                Date = t.UpdateTime
            }).ToArrayAsync();

            return retults;
        }
        public async Task<TodayWeathViewModel[]> GetToday()
        {
            if(DateTime.Now.Hour > 17&&DateTime.Now.Hour<8)
            {
                return null;
            }
            else
            {
                var model = await _context.TodayWeathers.Where(t => t.Today == DateTime.Today&&t.IsOverTime==false).Select(t => new TodayWeathViewModel
                {
                    Sky = t.Sky,
                    Day = t.Day,
                    Temperature = t.Temperature,
                    Today = t.Today,
                    Weather = t.Weather,
                    Wind = t.Wind
                }).ToArrayAsync();
                return model;
            }
           
        }
    }
}
