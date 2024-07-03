using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.RevitAPIUtils
{
    public class TransactionQueue<T>
    {
        private Queue<T> _queue = new Queue<T>();
        public event EventHandler<ItemEnqueuedEventArgs<T>> ItemEnqueued;
        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
            OnItemEnqueued(item);
        }

        public T Dequeue()
        {
            return _queue.Dequeue();
        }

        public int Count => _queue.Count;

        public T Peek()
        {
            return _queue.Peek();
        }

        protected virtual void OnItemEnqueued(T item)
        {
            ItemEnqueued?.Invoke(this, new ItemEnqueuedEventArgs<T>(item));
        }
    }

    public class ItemEnqueuedEventArgs<T> : EventArgs
    {
        public T EnqueuedItem { get; }

        public ItemEnqueuedEventArgs(T enqueuedItem)
        {
            EnqueuedItem = enqueuedItem;
        }
    }
}
