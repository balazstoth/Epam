using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace TimeSlotEnumerator
{
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
