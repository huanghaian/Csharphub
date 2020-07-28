using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using TestClassConsole.Entity.Virtuals;

namespace TestClassConsole
{
    class Program
    {
        private static Func<string, IEnumerable<string>> webGetString = MemoizeThreadSafe<string, IEnumerable<string>>(GetString);
        public static List<object> list;
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        private static async Task UoloadWithSinagle()
        {
            HttpClient httpClient = new HttpClient();
            var path = "C:\\Temp\\test.txt";
            System.IO.FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            var testContent = new MultipartFormDataContent(boundary);
            testContent.Headers.Remove("Content-Type");
            var reult = testContent.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
            testContent.Add(streamContent);
            var res = await httpClient.PostAsync("http://localhost:8500/api/Files/Upload", testContent);

            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine(res.StatusCode);
                Console.WriteLine(res.Content);
                Console.WriteLine(res.RequestMessage);
            }
        }
        private static async Task UoloadWithMulti()
        {
            HttpClient httpClient = new HttpClient();
            var path = "C:\\Temp\\test.txt";
            var path2 = "C:\\Temp\\test1.txt";
            var path3 = "C:\\Temp\\test.pptx";
            var streamContent1 = CreateStreamContent(path, "application/octet-stream");

            var mixed = new MultipartContent("mixed")
            {
                CreateStreamContent(path, "application/octet-stream"),
                CreateStreamContent(path2, "application/octet-stream"),
                CreateStreamContent(path3, "application/octet-stream"),

            };

            var testContent = new MultipartFormDataContent();
            testContent.Add(mixed,"files");
            //testContent.Headers.Remove("Content-Type");
            //var reult = testContent.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
            //testContent.Add(streamContent);

            var res = await httpClient.PostAsync("http://10.67.2.40:8500/api/Files/Upload", testContent);

            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine(res.StatusCode);
                Console.WriteLine(res.Content);
                Console.WriteLine(res.RequestMessage);
            }
        }
        public static StreamContent CreateStreamContent(string path,string contentType)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return streamContent;
        }

        //public static Func<string, Task<string>> myFunc = async (a) => { await Task.Run(); }

        public static int Test<T1, T2>(Func<T1,T2,int> func,T1 a,T2 t2)
        {
            return func(a,t2);
        }
        public static int Add(int a,int b)
        {
            return a + b;
        }

        public static string[] GetString(string url)
        {
            var list = new List<string>();
            list.Add(url);
            return list.ToArray();
        }
        public static Func<T,R> MemoizeThreadSafe<T,R>(Func<T,R> func) where T:IComparable
        {
            ConcurrentDictionary<T, R> cache = new ConcurrentDictionary<T, R>();
            return arg => cache.GetOrAdd(arg,a=>func(a));
        }

        private static void TestTaskDelay()
        {
            Task.Run(async() =>
            {
                Console.WriteLine("开始异步任务");
                await Task.Delay(2000);
                Console.WriteLine("异步任务结束");
                
            });
            Console.WriteLine($"{nameof(TestTaskDelay)}");
        }

        private static void TestTaskComplatetionSource()
        {
            Task.Run(async() =>
            {
                var tcs = new TaskCompletionSource<int>();
                new Thread(() =>
                {
                    Thread.Sleep(5000);
                    tcs.SetResult(42);
                })
                {
                    IsBackground = true
                }.Start();
                //Task<int> t= tcs.Task;
                int resul = await tcs.Task;
                Console.WriteLine(resul);
            });
        }
        private static void TestWhenAll()
        {
            Task.Run((async () =>
            {
                System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

                var task1 = Task.Run(() =>
                {
                    Console.WriteLine("task1");
                });
                var task2 = Task.Run(() =>
                {
                    Console.WriteLine("task2");

                });
                //等待Task.WhenAll(),结果会是当ask1,task2执行结束后才会继续往下执行
                await Task.WhenAll(task1, task2);
                Console.WriteLine("加载本地消息耗时：" + sw.ElapsedMilliseconds);
            }));
        }

        static void GetVal(out int v)
        {
            //out 关键词，传参进来的变量v不需要出事话，但是如果直接对v进行操作会有异常，比如使用v.ToString()【编译器会报错:使用未赋值的变量v】，然后在对V赋值；是不允许的。
           // v.ToString(); //这里调用ToString(),是不允许的。
            v = 10;
        }
        static void Add(ref int v)
        {
            //ref 关键词，传进来的变量v需要在调用之前初始化
            var mod = v.ToString();
            Console.WriteLine(mod);
            v += 78;
        }
    }
}
