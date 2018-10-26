using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Example3.IOBound;
using ColoredConsole;

namespace Example3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ColorConsole.WriteLine($"MainThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Entering Main()".Gray());
            Listener listener = new Listener();
            Sender sender = new Sender();
            var cancelled = false;

            CancellationTokenSource cts = new CancellationTokenSource();

            //Create UI thread
            Thread UIThread = new Thread(() =>
            {
                while(true)
                {
                    if (Console.ReadKey(true).KeyChar.ToString().ToUpperInvariant() == "F")
                    {
                        ColorConsole.WriteLine($"UIThread: Thread Id {Thread.CurrentThread.ManagedThreadId}".Yellow());
                        Sender.heartbeatInterval = 2000;
                    }

                    if (Console.ReadKey(true).KeyChar.ToString().ToUpperInvariant() == "C")
                    {
                        ColorConsole.WriteLine($"UIThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Cancellation has been requested".Red());
                        if (!cancelled) { cts.Cancel(); };
                        cancelled = true;
                        break;
                    }

                    if (Console.ReadKey(true).KeyChar.ToString().ToUpperInvariant() == "S")
                    {
                        ColorConsole.WriteLine($"UIThread: Thread Id {Thread.CurrentThread.ManagedThreadId}".Yellow());
                        Sender.heartbeatInterval = 1000;
                    }
                }                
            });
            UIThread.Start();

            //Create Listener thread to handle IO-bound operations
            Thread listenerThread = new Thread(new ParameterizedThreadStart(listener.Listen));
            listenerThread.Start(cts.Token);
            
            //Create Sender thread to handle IO-bound operations
            Thread senderThread = new Thread(new ParameterizedThreadStart(sender.Send));
            senderThread.Start(cts.Token);

            listenerThread.Join();
            senderThread.Join();
            cts.Dispose();
            ColorConsole.WriteLine($"MainThread: Thread Id {Thread.CurrentThread.ManagedThreadId} : Exiting Main()".Gray());        }
    }
}
