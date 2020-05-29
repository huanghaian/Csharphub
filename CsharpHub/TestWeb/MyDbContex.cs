using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeb.Models;

namespace TestWeb
{
    public class MyDbContex:DbContext
    {
        public MyDbContex(DbContextOptions<MyDbContex> options) : base(options)
        {

        }
        public DbSet<WeatherModel> WeatherModels { get; set; }
    }
}
