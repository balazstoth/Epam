using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericTree
{
    class Node<T> : ICollection<T>, IEquatable<T>
    {
        private List<Node<T>> children;
        private T value;
        protected int level;
        public bool IsLastProperty { get { return Parent.LastChild(this); } }

        Node<T> Parent { get; set; }
        public Node<T> this[int index] { get { return children[index]; } set { children[index] = value; } }

        public Node(Node<T> parent, T value)
        {
            level = parent == null ? 0: parent.level + 1;
            this.value = value;
            Parent = parent;
            children = new List<Node<T>>();
        }

        protected bool LastChild(Node<T> child)
        {
            return child == children[children.Count - 1];
        }
        public override string ToString()
        {
            return value.ToString();
        }
        public bool ContainsRecursive(T item)
        {
            if (value.Equals(item))
                return true;

            foreach (var child in children)
                if (child.ContainsRecursive(item))
                    return true;

            return false;
        }
        public bool ContainsRecursive(Func<T, bool> predicate)
        {
            if (predicate(value))
                return true;

            foreach (var child in children)
                if (child.ContainsRecursive(predicate))
                    return true;

            return false;
        }
        public void Display()
        {
            bool isLast = Parent == null ? false : Parent.LastChild(this);
            string currentLine = level > 0 ? "──" + value.ToString() : value.ToString();

            if (level > 0)
                if (isLast)
                    currentLine = "└" + currentLine;
                else
                    currentLine = "│" + currentLine;

            var p = Parent;
            while (p != null && p.Parent != null)
            {
                if (p.IsLastProperty)
                    currentLine = "   " + currentLine;
                else
                    currentLine = "│  " + currentLine;
                p = p.Parent;
            }

            Console.WriteLine(currentLine);
            foreach (var child in children)
                child.Display();
        }
       
        #region ICollection implementation
        public int Count => children.Count;
        public bool IsReadOnly => false;
        public IEnumerator<T> GetEnumerator()
        {
            return new NodeEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public void Add(T item)
        {
            children.Add(new Node<T>(this, item));
        }
        public void Clear()
        {
            children.Clear();
        }
        public bool Contains(T item)
        {
            return children.Find(x => x.value.Equals(item)) != null;
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(T item)
        {
            return children.Remove(new Node<T>(null, item));
        }
        public bool Equals(T other)
        {
            return this.value.Equals(other);
        }
        #endregion

        class NodeEnumerator : IEnumerator<T>
        {
            private const int startIndex = -1;
            private Node<T> container;
            private int index;

            public NodeEnumerator(Node<T> container)
            {
                this.container = container;
                index = startIndex;
            }

            public T Current => container.children[index].value;
            object IEnumerator.Current => this.Current;
            public void Dispose()
            {
            }
            public bool MoveNext()
            {
                return ++index < container.children.Count;
            }
            public void Reset()
            {
                index = startIndex;
            }
        }
    }
}
