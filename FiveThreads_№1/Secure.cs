using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiveThreads__1
{
    class Secure
    {
        IList<double> _listS = new List<double>();
        IList<double> _listR = new List<double>();
        private static object _threadlocker = new object();
        private static object _threadlocker2 = new object();


        public IList<double> ListS { get => _listS; set => _listS = value; }
        public IList<double> ListR { get => _listR; set => _listR = value; }


        public void Test()
        {
            var A = new Thread(() =>
            {
                lock (_threadlocker)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ListS.Add(i);
                        Console.WriteLine("Thread A: Add in ListS: " + i);
                    }
                }
            });
            A.Start();

            var B = new Thread(() =>
            {
                double last = 0;
                Thread.Sleep(2000);
                lock (_threadlocker)
                {
                    while (ListS.Count == 0)
                    {
                        Console.WriteLine("Thread B: No element in ListS!!!");
                        Console.WriteLine("Thread B: Sleep(1000)");
                        Thread.Sleep(1000);
                    }
                    last = ListS.Last() * ListS.Last();
                }

                lock (_threadlocker2)
                {
                    while (ListR.Count() == 0)
                    {
                        Console.WriteLine("Thread B: No element in ListR!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread B: Sleep(1000)");
                    }
                    ListR.Add(last);
                    Console.WriteLine("Thread B: Add in ListR last element ListS " + last);
                }
            });
            B.Start();

            var C = new Thread(() =>
            {
                double last = 0;
                lock (_threadlocker)
                {
                    if (ListS.Count == 0)
                    {
                        Console.WriteLine("Thread C: No element in ListS!!!");
                        Thread.Sleep(2000);
                        Console.WriteLine("Thread C: Sleep(1000)");
                    }
                    last = ListS.Last();
                }

                lock (_threadlocker2)
                {
                    if (ListR.Count() == 0)
                    {
                        Console.WriteLine("Thread C: No element in ListR!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread C: Sleep(1000)");
                    }
                    ListR.Add(last / 3);
                    Console.WriteLine("Thread C: Add in ListR last element ListS " + last / 3);
                }
            });
            C.Start();
            C.Join();

            var D = new Thread(() =>
            {
                lock (_threadlocker2)
                {
                    while (ListR.Count == 0)
                    {
                        Console.WriteLine("Thread D: No element in ListR!!!");
                        Console.WriteLine("Thread D: Sleep(1000)");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("Thread D: print last element ListR: " + ListR.Last());
                }
            });
            D.Start();
        }

        public void TestLock() 
        {
            var A = new Thread(() =>
            {
                lock (_threadlocker)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ListS.Add(i);
                        Console.WriteLine("Thread A: Add in ListS: " + i);
                    }
                }
                Console.WriteLine("Thread A fread lock();");
            });
            A.Start();

            var B = new Thread(() =>
            {
                double last = 0;
                Thread.Sleep(2000);
                lock (_threadlocker)
                {
                    while (ListS.Count == 0)
                    {
                        Console.WriteLine("Thread B: No element in ListS!!!");
                        Console.WriteLine("Thread B: Sleep(1000)");
                        Thread.Sleep(1000);
                    }
                    last = ListS.Last() * ListS.Last();
                }

                lock (_threadlocker2)
                {
                    while (ListR.Count() == 0)
                    {
                        Console.WriteLine("Thread B: No element in ListR!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread B: Sleep(1000)");
                    }
                    ListR.Add(last);
                    Console.WriteLine("Thread B: Add in ListR last element ListS " + last);
                }
            });
            B.Start();

            var C = new Thread(() =>
            {
                double last = 0;
                lock (_threadlocker)
                {
                    if (ListS.Count == 0)
                    {
                        Console.WriteLine("Thread C: No element in ListS!!!");
                        Thread.Sleep(2000);
                        Console.WriteLine("Thread C: Sleep(1000)");
                    }
                    last = ListS.Last();
                }

                lock (_threadlocker2)
                {
                    if (ListR.Count() == 0)
                    {
                        Console.WriteLine("Thread C: No element in ListR!!!");
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread C: Sleep(1000)");
                    }
                    ListR.Add(last / 3);
                    Console.WriteLine("Thread C: Add in ListR last element ListS " + last / 3);
                }
            });
            C.Start();

            var D = new Thread(() =>
            {
                lock (_threadlocker2)
                {
                    while (ListR.Count == 0)
                    {
                        Console.WriteLine("Thread D: No element in ListR!!!");
                        Console.WriteLine("Thread D: Sleep(1000)");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("Thread D: print last element ListR: " + ListR.Last());
                }
            });
            D.Start();
            Console.ReadKey();
            D.Abort();
        }
    }
}





