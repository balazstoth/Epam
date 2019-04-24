using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TimeSlotEnumerator
{
    public enum Direction { Left, Right, OnlyLeft, OnlyRight }

    class TimeSlot<T> : IEnumerable<T>, ICollection<T>
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

    class TimeSlotEnumerator<T> : IEnumerator<T>
    {
        public readonly int startIndex;
        public int currentIndex;
        List<T> collection;
        Direction direction;
        int step;
        readonly int slotNumber;

        public TimeSlotEnumerator(IEnumerable<T> collection, int startIndex, int slotNumber)
        {
            this.startIndex = startIndex;
            this.currentIndex = startIndex;
            this.collection = collection.ToList();
            direction = Direction.Right;
            step = 0;
            this.slotNumber = slotNumber;
        }

        public T Current
        {
            get
            {
                return (currentIndex >= collection.Count || currentIndex < 0) ? default : collection[currentIndex];
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            int count = collection.Count;
            switch (direction)
            {
                case Direction.Left:
                    if (currentIndex - step < 0)
                    {
                        currentIndex++;
                        direction = Direction.OnlyRight;
                    }
                    else
                    {
                        currentIndex -= step++;
                        direction = Direction.Right;
                    }
                    break;

                case Direction.Right:
                    if (currentIndex + step >= count)
                    {
                        currentIndex--;
                        direction = Direction.OnlyLeft;
                    }
                    else
                    {
                        currentIndex += step++;
                        direction = Direction.Left;
                    }
                    break;

                case Direction.OnlyLeft:
                    step++;
                    currentIndex--;
                    break;

                case Direction.OnlyRight:
                    step++;
                    currentIndex++;
                    break;
            }

            if (currentIndex < 0 || currentIndex >= count || step == slotNumber + 1)
                return false;

            return true;
        }

        public void Reset()
        {
            currentIndex = startIndex;
        }
    }
}
