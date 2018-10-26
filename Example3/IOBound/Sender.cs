using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ColoredConsole;

namespace Example3.IOBound
{
    public class Sender
    {
        public static int heartbeatInterval { get; set; } = 1000;
        List<Task> tasks = new List<Task>();

        public void Send(Object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            ColorConsole.WriteLine($"SenderThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Entering Send()".Yellow());
            while(!ct.IsCancellationRequested)
            {
                //Send the desired message
                tasks.Add(Task.Factory.StartNew(() => SendMessage($"Heartbeat"), ct));
                Task.WaitAll(tasks.ToArray());
            }
            ColorConsole.WriteLine($"SenderThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Exiting Send()".Yellow());
            Console.ReadKey(true);
        }

        public void SendMessage(string message)
        {
            ColorConsole.WriteLine($"SenderThread: Thread Id {Thread.CurrentThread.ManagedThreadId}: TimeStamp: {DateTime.Now}: Sending {message} at interval {heartbeatInterval}".Green());
            Thread.Sleep(heartbeatInterval);
        }

    }
}
