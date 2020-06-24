using CAPPWebApi.Models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CAPPWebApi.Backgroups
{
    public class WeaterDataService : IDataService
    {
        public async Task RunTask()
        {
            try
            {
                var builder = new DbContextOptionsBuilder<MyDbContext>();
                builder.UseSqlite($"data source =Db" + "/" + "TestWeb.db");
                var context = new MyDbContext(builder.Options);
                var results = GetTodayWeather();
                await context.TodayWeathers.AddRangeAsync(results);
                var moreDayWeather = GetDayWeathers();
                await context.Weathers.AddRangeAsync(moreDayWeather);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private Weather[] GetDayWeathers()
        {
            var htmlStr = RequestData("http://www.weather.com.cn/weather/101300106.shtml");
            try
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlStr);
                var res = htmlDoc.DocumentNode.SelectNodes("//div[@id='7d']/ul/li");
                List<Weather> list = new List<Weather>();
                foreach (var elememt in res)
                {
                    var childDoc = new HtmlDocument();
                    childDoc.LoadHtml(elememt.InnerHtml);
                    var model = new Weather()
                    {
                        Day = childDoc.DocumentNode.SelectSingleNode("//h1").InnerText.Trim(),
                        Weath = childDoc.DocumentNode.SelectSingleNode("//p[@class='wea']").InnerText.Trim(),
                        Temperature = childDoc.DocumentNode.SelectSingleNode("//p[@class='tem']").InnerText.Trim(),
                        Wind = (childDoc.DocumentNode.SelectNodes("//p[@class='win']/em/span").FirstOrDefault()?.Attributes["title"].Value ?? ""),
                        WindLevel = childDoc.DocumentNode.SelectSingleNode("//p[@class='win']/i").InnerText,
                        UpdateTime = DateTime.Now,
                    };
                    list.Add(model);
                }
                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TodayWeather[] GetTodayWeather()
        {

            string htmlStr = RequestData("http://www.weather.com.cn/weather1d/101300106.shtml");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlStr);
            var res = htmlDoc.DocumentNode.SelectNodes("//div[@id='today']/div[@class='t']/ul/li");
            var model = new List<TodayWeather>();
            foreach (var li in res)
            {
                var liHtmlDoc = new HtmlDocument();
                liHtmlDoc.LoadHtml(li.InnerHtml);
                var day = liHtmlDoc.DocumentNode.SelectSingleNode("//h1").InnerText;
                var wea = liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='wea']").InnerText;
                var tem = liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='tem']/span").InnerText + liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='tem']/em").InnerText;
                var win = liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='win']/span").Attributes["title"].Value + liHtmlDoc.DocumentNode.SelectSingleNode("//p[@class='win']/span").InnerText;
                var sky = liHtmlDoc.DocumentNode.SelectSingleNode("//div[@class='sky']/span[@class='txt lv3']")?.InnerText ?? "";
                var date = DateTime.Today;
                var todayWeather = new TodayWeather()
                {
                    Id = Guid.NewGuid().ToString(),
                    Day = day,
                    Weather = wea,
                    Temperature = tem,
                    Wind = win,
                    Sky = sky,
                    Today = date,
                    UpdateTime = DateTime.Now
                };
                if (day.Contains(DateTime.Now.Day + "日白天"))
                {
                    todayWeather.DayType = DayType.CurrentDay;
                }
                else if (day.Contains(DateTime.Now.Day + "日夜间"))
                {
                    todayWeather.DayType = DayType.CurrentNight;

                }
                else
                {
                    todayWeather.DayType = DayType.NewCurrentDay;
                }
                model.Add(todayWeather);
            }
            return model.ToArray();

        }

        private static string RequestData(string url)
        {
            WebRequest request = WebRequest.Create(url);            //实例化WebRequest对象  
            WebResponse response = request.GetResponse();           //创建WebResponse对象  
            Stream datastream = response.GetResponseStream();       //创建流对象  
            Encoding ec = Encoding.UTF8;
            StreamReader reader = new StreamReader(datastream, ec);
            var htmlStr = reader.ReadToEnd();                  //读取网页内容  
            reader.Close();
            datastream.Close();
            response.Close();
            return htmlStr;
        }

        public async Task UpdateWheatherInNight()
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseSqlite($"data source =Db" + "/" + "TestWeb.db");
            var context = new MyDbContext(builder.Options);
            var qurey=context.TodayWeathers.Where(t => t.Today.Date == DateTime.Now.Date&&t.DayType==DayType.CurrentDay).ToArray();
            if (qurey.Any())
            {
                var oldTodays = qurey.ToArray();
                foreach (var item in oldTodays)
                {
                    item.IsOverTime = true;
                }
                context.TodayWeathers.UpdateRange(oldTodays);
            }
            //var rootNodePath = "//div[@class='t']/ul[@class='clearfix']/li";
            //var htmlStr = RequestData("http://www.weather.com.cn/weather1d/101300106.shtml");
            //var htmlDoc = new HtmlDocument();
            //htmlDoc.LoadHtml(htmlStr);
            //var nodes = htmlDoc.DocumentNode.SelectNodes(rootNodePath);
            //if (models != null)
            //{
            //    foreach(var )
            //}
            var data = GetTodayWeather();
            if (data.Length > 0)
            {
                foreach(var item in data)
                {
                    if (item.Day.Contains(DateTime.Now.Day + "日夜间"))
                        continue;
                    context.TodayWeathers.Add(item);
                }
                await context.SaveChangesAsync();
            }
        }

    }
}
