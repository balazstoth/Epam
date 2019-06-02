using System;

namespace Kitchen
{
    public class OvenFinishedEventArgs : EventArgs
    {
    }

    public class IngredienIsPreparedEventArgs<T> : EventArgs
    {
        T item;

        public IngredienIsPreparedEventArgs(T item)
        {
            this.item = item;
        }
    }
}
