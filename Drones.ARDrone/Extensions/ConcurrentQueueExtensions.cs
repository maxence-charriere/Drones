using System.Collections.Concurrent;

namespace Drones.ARDrone.Extensions
{
    public static class ConcurrentQueueExtensions
    {
        public static void Flush<T>(this ConcurrentQueue<T> queue)
        {
            T item;
            while (queue.TryDequeue(out item))
            {
            }
        }
    }
}
