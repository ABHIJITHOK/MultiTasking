using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ColoredConsole;

namespace Example3.IOBound
{
    public class Listener
    {
        public void Listen(Object obj)
        {
            CancellationToken ct = (CancellationToken)obj;
            ColorConsole.WriteLine($"ListenerThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Entering Listen()".Magenta());
            while (!ct.IsCancellationRequested)
            {
                ListenForMessages($"ACK");
            }
            ColorConsole.WriteLine($"ListenerThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Exiting Listen()".Magenta());
            Console.ReadKey(true);
        }

        public async void ListenForMessages(string message)
        {
            await Task.Run(() =>
                {
                    ColorConsole.WriteLine($"ListenerThread: Thread Id {Thread.CurrentThread.ManagedThreadId}: TimeStamp: {DateTime.Now}: Listening for {message}".Red());
                    Thread.Sleep(1000);
                }
            );
        }
    }
}
