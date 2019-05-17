using System;

namespace Kitchen
{
    class Oven
    {
        private readonly int capacity;

        public int OvenCapacity { get { return capacity; } }

        public Oven(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(capacity.ToString());

            this.capacity = capacity;
        }
    }
}
