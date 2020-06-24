using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aiursoft.HSharp.Methods;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestWeb.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyDbContex _context;
        private readonly IConfiguration _configuration;
        public IndexModel(ILogger<IndexModel> logger, MyDbContex myDbContex, IConfiguration configuration)
        {
            _logger = logger;
            _context = myDbContex;
            _configuration = configuration;
        }
        //public async Task onGet()
        //{
        //    GetToday();
        //    await Task.CompletedTask;
        //}
        public async Task OnGet()
        {
            _ = Task.Run(() =>
            {
                GetToday();
            });
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //WebRequest request = WebRequest.Create("http://www.weather.com.cn/weather/101300106.shtml");            //实例化WebRequest对象  
            //WebResponse response = request.GetResponse();           //创建WebResponse对象  
            //Stream datastream = response.GetResponseStream();       //创建流对象  
            //Encoding ec = Encoding.UTF8;
            //StreamReader reader = new StreamReader(datastream, ec);
            //var htmlStr = reader.ReadToEnd();                  //读取网页内容  
            //reader.Close();
            //datastream.Close();
            //response.Close();
            //_ = Task.Run(async () =>
            //  {
            //      try
            //      {
            //          var builder = new DbContextOptionsBuilder<MyDbContex>();
            //          builder.UseSqlite($"data source ={_configuration["Storage:Db"]}" + "/" + "TestWeb.db");
            //          var context = new MyDbContex(builder.Options);
            //          var htmlDoc = new HtmlDocument();
            //          htmlDoc.LoadHtml(htmlStr);
            //          var res = htmlDoc.DocumentNode.SelectNodes("//div[@id='7d']/ul/li");
            //          List<WeatherModel> list = new List<WeatherModel>();
            //          foreach (var elememt in res)
            //          {
            //              var childDoc = new HtmlDocument();
            //              childDoc.LoadHtml(elememt.InnerHtml);
            //              var model = new WeatherModel()
            //              {
            //                  Day = childDoc.DocumentNode.SelectSingleNode("//h1").InnerText.Trim(),
            //                  Weath = childDoc.DocumentNode.SelectSingleNode("//p[@class='wea']").InnerText.Trim(),
            //                  Temperature = childDoc.DocumentNode.SelectSingleNode("//p[@class='tem']").InnerText.Trim(),
            //                  Wind = (childDoc.DocumentNode.SelectNodes("//p[@class='win']/em/span").FirstOrDefault()?.Attributes["title"].Value ?? ""),
            //                  WindLevel = childDoc.DocumentNode.SelectSingleNode("//p[@class='win']/i").InnerText,
            //                  UpdateTime = DateTime.Now,
            //              };
            //              list.Add(model);
            //          }

            //          context.WeatherModels.AddRange(list);

            //          await context.SaveChangesAsync();

            //      }
            //      catch (Exception ex)
            //      {
            //          Console.WriteLine(ex.StackTrace);
            //      }

            //  });
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
        public void GetToday()
        {
            WebRequest request = WebRequest.Create("http://www.weather.com.cn/weather1d/101300106.shtml");            //实例化WebRequest对象  
            WebResponse response = request.GetResponse();           //创建WebResponse对象  
            Stream datastream = response.GetResponseStream();       //创建流对象  
            Encoding ec = Encoding.UTF8;
            StreamReader reader = new StreamReader(datastream, ec);
            var htmlStr = reader.ReadToEnd();                  //读取网页内容  
            reader.Close();
            datastream.Close();
            response.Close();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlStr);
            var res = htmlDoc.DocumentNode.SelectNodes("//div[@id='today']/div[@class='t']/ul/li");
            foreach(var li in res)
            {
                var liHtmlDoc = new HtmlDocument();
                liHtmlDoc.LoadHtml(li.InnerHtml);
                var day = liHtmlDoc.DocumentNode.SelectSingleNode("//h1").InnerText;
                var wea = liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='wea']").InnerText;
                var tem = liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='tem']/span").InnerText+ liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='tem']/em").InnerText;
                var win = liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='win']/span").Attributes["title"].Value + liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='win']/span").InnerText;
                var sky = liHtmlDoc.DocumentNode.SelectSingleNode("//div[@class='sky']/span[@class='txt lv3']")?.InnerText??"";
                var date = DateTime.Today;
            }

        }
    }
}
