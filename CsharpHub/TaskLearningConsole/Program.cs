using System;
using System.Threading.Tasks;

namespace TaskLearningConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTaskRunWithWati();
            TestTaskRunWithResult();
            Console.WriteLine("主线程");
            Console.ReadKey();
        }

        static void TestTaskRunNoWait()
        {
            //Task 默认使用线程池的线程都是后台线程，一旦主线程执行结束，Task也随之停止。在Main函数中调用，如果主线程不阻塞，下面的语句不会输出。
            Task.Run(() => {
                Console.WriteLine("Task run");
            });
        }
        static void TestTaskRunWithWati()
        {
           var task =  Task.Run(() => Console.WriteLine("Task Start"));
            //Wati()阻塞当前方法，及等待task完成，在主线程调用的结果是：（1）"Task Start" (2) "Task Wait 阻塞当前方法" (3) "主线程"
            //如果不执行Wati方法，调用结果为：（1）"Task Wait 阻塞当前方法" (2)"主线程"  (3)"Task Start" 
            task.Wait();
           Console.WriteLine("Task Wait 阻塞当前方法");
        }
        static void TestTaskRunWithResult ()
        {
            var task= Task.Run(()=> { Console.WriteLine("Task Run With Result"); return 1; });
            //调用Result 阻塞当前方法，同Wati()
            int result = task.Result;
            Console.WriteLine("执行Task Resut");
        }

    }
}
