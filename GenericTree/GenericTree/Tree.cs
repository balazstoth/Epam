using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericTree
{
    class Tree<T> : IEnumerable<T>
    {
        public Node<T> Root { get; set; }

        public Tree(T initValue)
        {
            Root = new Node<T>(null, initValue);
        }

        public bool Contains(T item)
        {
            return Root.ContainsRecursive(item);
        }
        public bool Contains(Func<T, bool> predicate)
        {
            return Root.ContainsRecursive(predicate);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Root.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public override string ToString()
        {
            return Root.ToString();
        }
        public void Display()
        {
            Root.Display();
        }
    }
}
