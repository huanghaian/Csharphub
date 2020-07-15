using System;
using System.Threading;
using System.Threading.Tasks;
using TestClassConsole.Entity.Virtuals;

namespace TestClassConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //int x;
            //GetVal(out x);
            //Console.WriteLine(x);
            //Add(ref x);
            //Console.WriteLine(x);
            //TestWhenAll();
            TestTaskComplatetionSource();
            Console.ReadKey();
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
