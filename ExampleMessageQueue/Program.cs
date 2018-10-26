using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Collections.Concurrent;

namespace ExampleMessageQueue
{
    public class Program
    {
        public Queue<string> sendQueue = new Queue<string>();
        public static void Main(string[] args)
        {
            StandardQueue();
            ConcurrentQueue();
        }

        public static void StandardQueue()
        {
            Queue<string> myQueue = new Queue<string>();

            //Add items to the queue
            myQueue.Enqueue("Abhy");
            myQueue.Enqueue("Sangs");
            myQueue.Enqueue("Unni");

            //Remove first item from the queue
            var queueElement = myQueue.Dequeue();

            //Peek at the next element in the queue
            var nextQueueElement = myQueue.Peek();

            //Remove next item from the queue
            nextQueueElement = myQueue.Dequeue();

            //Check if queue contains an element
            var isContaining = myQueue.Contains<string>("Unni");
            myQueue.Dequeue();
            isContaining = myQueue.Contains<string>("Unni");
        }

        public static void ConcurrentQueue()
        {
            ConcurrentQueue<string> myQueue = new ConcurrentQueue<string>();

            //Add items to the concurrent queue
            myQueue.Enqueue("Abhy");
            myQueue.Enqueue("Sangs");
            myQueue.Enqueue("Unni");

            //Check if the queue contains an item
            var isContaining = myQueue.Contains<string>("Abhy");
            isContaining = myQueue.Contains<string>("Sangs");
            isContaining = myQueue.Contains<string>("Unni");

            string item = string.Empty;

            //Peek for next item on concurrent queue
            var isPeekSuccess = myQueue.TryPeek(out item);

            //Remove items from the concurrent queue
            var isDequeueSuccess = myQueue.TryDequeue(out item);
            isDequeueSuccess = myQueue.TryDequeue(out item);
            isDequeueSuccess = myQueue.TryDequeue(out item);
            isDequeueSuccess = myQueue.TryDequeue(out item);

            var isNextItemAvailable = myQueue.TryPeek(out item);

        }
    }
}
