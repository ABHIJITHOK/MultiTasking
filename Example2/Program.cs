using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Example2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId} : Entering Main.");

            Simulator LOCSAdapterSim = new Simulator(heartbeatInterval: 1);
            CancellationTokenSource cts = new CancellationTokenSource();
            
            //Create a User Interface thread that monitors for User Input via Console
            Console.WriteLine("Press 'C' to terminate the application");
            Thread UIThread = new Thread(()=>
                {
                    if (Console.ReadKey(true).KeyChar.ToString().ToUpperInvariant() == "C")
                    {
                        cts.Cancel();
                    }
                }
            );

            Thread SenderThread = new Thread(new ParameterizedThreadStart(LOCSAdapterSim.Sender));
            UIThread.Start();
            SenderThread.Start(cts.Token);

            Console.WriteLine($"Main thread with Thread Id: {Thread.CurrentThread.ManagedThreadId} is doing something.");
            SenderThread.Join();
            cts.Dispose();
            Console.WriteLine($"Main thread with Thread Id: {Thread.CurrentThread.ManagedThreadId} is done.");
        }
    }
}
