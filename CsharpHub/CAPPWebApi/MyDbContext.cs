using CAPPWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPPWebApi
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<TodayWeather> TodayWeathers { get; set; }

    }
}