using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Example2
{
    public partial class Simulator
    {
        public static int HeartbeatInterval { get; set; } = 1;
        public static bool SendHeartBeat { get; set; } = true;
        public static CancellationToken ct;

        public Simulator(int heartbeatInterval)
        {
            HeartbeatInterval = heartbeatInterval;
        }

        public void Sender(object obj)
        {
            Console.Write($"\nWorker Thread Id: {Thread.CurrentThread.ManagedThreadId}: Sender().");
            ct = (CancellationToken)obj;
            while (!ct.IsCancellationRequested)
            {
                //Periodic Tasks
                SendPeriodicMessages();

                //Aperiodic Tasks
            }
        }

        public void SendPeriodicMessages()
        {
            Console.Write($"\nWorker Thread Id: {Thread.CurrentThread.ManagedThreadId}: Sending Periodic Messages.");
            List<Task> tasks = new List<Task>();
            if (SendHeartBeat)
            {
                tasks.Add(Task.Factory.StartNew(() => HeartBeat(), ct));
            }
            Task.WaitAll(tasks.ToArray());
        }

        public void HeartBeat()
        {
            Console.WriteLine($"\nWorker Thread Id: {Thread.CurrentThread.ManagedThreadId}: Entering SendHeartBeat().");
            int i = 0;
            while (!ct.IsCancellationRequested && i < 20)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\nWorker Thread Id: {Thread.CurrentThread.ManagedThreadId}:Sending heartbeat {i}.");
                i++;
                Thread.Sleep(HeartbeatInterval * 1000);
            }
            Console.WriteLine($"Worker Thread Id: {Thread.CurrentThread.ManagedThreadId} : Exiting SendHeartBeat().");
        }
    }
}
