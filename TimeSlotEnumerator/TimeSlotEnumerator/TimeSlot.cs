using System.Collections;
using System.Collections.Generic;

namespace TimeSlotEnumerator
{
    public enum Direction { Left, Right, OnlyLeft, OnlyRight }

    class TimeSlot<T> : ICollection<T>
    {
        List<T> collection;
        int index;
        int slotNumber;

        public TimeSlot(int index, int slotNumber)
        {
            this.index = index;
            this.collection = new List<T>();
            this.slotNumber = slotNumber;
        }

        public int Count => collection.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            collection.Add(item);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TimeSlotEnumerator<T>(collection, index, slotNumber);
        }

        public bool Remove(T item)
        {
            return collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
