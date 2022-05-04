using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestTimeoutTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Start().Wait();
            Console.ReadKey();
        }

        public static async Task Start()
        {
            Task _ = Count();
        }

        public static async Task Count()
        {
            Task _ = new Task(() =>
            {
                int count = 0;
                while (count <5)
                {
                    Console.WriteLine($"Count: {count}");
                    count++;
                    Thread.Sleep(1000);
                }
            });
            if(await Task.WhenAny(_,Task.Delay(3500)) == _){
                Console.WriteLine("Complete!");
            }
            else
            {
                Console.WriteLine("Timeout!");
            }

        }
    }
}
