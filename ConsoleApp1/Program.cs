using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Entering Main");
            DoParentWork();
            Console.WriteLine("Exiting Main");
        }

        public static void DoParentWork()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            List<Task> tasks = new List<Task>();
            Console.WriteLine("Entering Parent");
            tasks.Add(Task.Factory.StartNew(() => DoChild1Work(), TaskCreationOptions.AttachedToParent));
            tasks.Add(Task.Factory.StartNew(() => DoChild2Work(), TaskCreationOptions.AttachedToParent));
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Exiting Parent");
        }


        public static void DoChild1Work()
        {
            Console.WriteLine("Entering Child1");
            int i = 0;
            while (i < 20)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Child 1 task is executing.");
                i++;
                Thread.Sleep(500);
            }
            Console.WriteLine("Exiting Child1");
        }

        public static void DoChild2Work()
        {
            Console.WriteLine("Entering Child2");
            int i = 0;
            while (i < 20)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Child 2 task is executing.");
                i++;
                Thread.Sleep(500);
            }
            Console.WriteLine("Exiting Child2");
        }
    }
}
