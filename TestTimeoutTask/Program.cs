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
            Task _ = Count();
            Console.ReadKey();
        }

        public static async Task Count()
        {
            try
            {
                using (CancellationTokenSource cancelResource = new CancellationTokenSource())
                {
                    bool _iscomplete = false;
                    Thread _ = new Thread(() =>
                    {
                        int count = 0;
                        while (count < 5)
                        {
                            Console.WriteLine($"Count: {count}");
                            count++;
                            Thread.Sleep(1000);
                        }
                        _iscomplete = true;
                    });
                    _.IsBackground = true;
                    _.Start();
                    await Task.Delay(2500);
                    if (!_iscomplete)
                    {
                        _.Abort();
                        throw new TimeoutException();
                    }
                }
            }
            catch(OperationCanceledException c)
            {
                
            }
            catch(TimeoutException t)
            {
                Console.WriteLine("Timeout!");
            }

        }
    }
}
