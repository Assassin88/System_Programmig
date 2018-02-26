using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiveThreads__1
{
    class Insecure
    {
        IList<double> _listS = new List<double>();
        IList<double> _listR = new List<double>();

        public IList<double> ListS { get => _listS; set => _listS = value; }
        public IList<double> ListR { get => _listR; set => _listR = value; }

        public void Test()
        {
            var A = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    ListS.Add(i);
                    Console.WriteLine("Thread A: Add in ListS: " + i);
                }
            });
            A.Start();

            var B = new Thread(() =>
            {
                if (ListS.Count == 0)
                {
                    Console.WriteLine("Thread B: No element in ListS!!!");
                    Console.WriteLine("Thread B: Sleep(1000)");
                    Thread.Sleep(1000);
                }
                var lastElement = ListS.Last();
                if (lastElement != 0)
                {
                    ListR.Add(lastElement * lastElement);
                }
                Console.WriteLine("Thread B: Add in ListR last element ListS " + lastElement * lastElement);
            });
            B.Start();

            var C = new Thread(() =>
            {
                while (ListS.Count == 0)
                {
                    Console.WriteLine("Thread C: No element in ListS!!!");
                    Thread.Sleep(1000);
                    Console.WriteLine("Thread C: Sleep(1000)");
                }
                while (ListR.Count() == 0)
                {
                    Console.WriteLine("Thread D: No element in ListR!!!");
                    Console.WriteLine("Thread C: Sleep(1000)");
                }
                var lastElement = ListR.Last();
                if (lastElement != 0)
                {
                    ListR.Add(lastElement / 3);
                }
                Console.WriteLine("Thread C: Add in ListR last element ListS " + lastElement / 3);
            });
            C.Start();

            var D = new Thread(() =>
            {
                if (ListR.Count == 0)
                {
                    Console.WriteLine("Thread D: No element in ListR!!!");
                    Console.WriteLine("Thread D: Sleep(1000)");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Thread D: print last element ListR: " + ListR.Last());
            });
            D.Start();
        }
    }
}
