using System;
using System.Collections.Generic;

namespace Kitchen.Notify
{
    class ObserveableQueue<T> where T : class
    {
        private Queue<T> queue;

        public event IngredientIsPreparedDelegate<T> IngredientHasPreparedEvent;
        public int Count => queue.Count;

        public ObserveableQueue()
        {
            queue = new Queue<T>();
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
            OnIngredientHasPrepared(new IngredienIsPreparedEventArgs<T>(item));
        }

        public T Dequeue()
        {
            return queue.Dequeue();
        }

        public T Peek()
        {
            try
            {
                T next = queue.Peek();
                return next;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        protected void OnIngredientHasPrepared(IngredienIsPreparedEventArgs<T> e)
        {
            var eventHandler = IngredientHasPreparedEvent;
            eventHandler?.Invoke(this, e);
        }
    }
}
