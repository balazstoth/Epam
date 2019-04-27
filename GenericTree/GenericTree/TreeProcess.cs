using System;

namespace GenericTree
{
    class TreeProcess<T>
    {
        Tree<T> tree;
        int numberOfProcessedItems;

        public TreeProcess(Tree<T> tree)
        {
            this.tree = tree;
            numberOfProcessedItems = 0;
        }

        public void DisplayTree()
        {
            tree.Root.Display();
        }
        public bool BreatdhFirst(Node<T> node, bool isTopLevel)
        {
            bool res = false;

            if (!node.processed)
            {
                node.processed = true;
                Console.WriteLine(node.value);
                numberOfProcessedItems++;
                if (!isTopLevel)
                    return tree.Count == numberOfProcessedItems;
            }

            while (!res)
            {
                foreach (var i in node.children)
                    res = BreatdhFirst(i, false);

                if (!res && !isTopLevel)
                    return false;
            }
            return true;
        }
    }
}
