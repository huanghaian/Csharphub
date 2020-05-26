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

namespace TestWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            WebRequest request = WebRequest.Create("http://www.weather.com.cn/weather/101300106.shtml");            //实例化WebRequest对象  
            WebResponse response = request.GetResponse();           //创建WebResponse对象  
            Stream datastream = response.GetResponseStream();       //创建流对象  
            Encoding ec = Encoding.UTF8;

            StreamReader reader = new StreamReader(datastream, ec);
            var htmlStr = reader.ReadToEnd();                  //读取网页内容  
            reader.Close();
            datastream.Close();
            response.Close();
            _=Task.Run(() =>
            {
                try
                {
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlStr);
                    var res=  htmlDoc.DocumentNode.SelectNodes("//div[@id='7d']/ul/li");
                    Console.WriteLine(htmlDoc);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            });
           
            //if (re.IsSuccessStatusCode)
            //{
            //    if (re.RequestMessage!=null)
            //    {
            //        if (!Directory.Exists("E://test"))
            //            Directory.CreateDirectory("E://test");
            //        using (var stream = System.IO.File.Open("E://test//" + Guid.NewGuid() + ".txt", FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            //        {
            //            _ = Task.Run(async () =>
            //              {
            //                 var model = await re.Content.ReadAsByteArrayAsync();
            //                  stream.Write(model);
            //               });
            //        }
            //}
            //    }
        }
    }
}
