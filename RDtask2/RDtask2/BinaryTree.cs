using System;
using System.Collections;
using System.Collections.Generic;

namespace RDtask2
{
    [Serializable]
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException() { }
        public DuplicateItemException(string message) : base(message) { }
        public DuplicateItemException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateItemException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    internal class Node<T>
    {
        internal T key;
        internal Node<T> left;
        internal Node<T> right;

        internal Node(T key)
        {
            this.key = key;
        }
    }

    public abstract class BinaryTreeChangedEventArgs : EventArgs
    {
        public DateTime Time { get; } = DateTime.Now;
    }

    public class BinaryTreeClearedEventArgs : BinaryTreeChangedEventArgs { }

    public abstract class BinaryTreeChangedOnItemEventArgs<T> : BinaryTreeChangedEventArgs
    {
        public T Item { get; }

        public BinaryTreeChangedOnItemEventArgs(T item)
        {
            Item = item;
        }
    }

    public class BinaryTreeItemAddedEventArgs<T> : BinaryTreeChangedOnItemEventArgs<T>
    {
        public BinaryTreeItemAddedEventArgs(T item) : base(item) { }
    }

    public class BinaryTreeItemRemovedEventArgs<T> : BinaryTreeChangedOnItemEventArgs<T>
    {
        public BinaryTreeItemRemovedEventArgs(T item) : base(item) { }
    }

    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        public event EventHandler<BinaryTreeItemAddedEventArgs<T>> Added;

        public event EventHandler<BinaryTreeItemRemovedEventArgs<T>> Removed;

        public event EventHandler<BinaryTreeClearedEventArgs> Cleared;

        private Node<T> root;

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public bool IsEmpty() => root == null;

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (root == null)
                root = new Node<T>(item);
            else
                Add(root, item);

            Count++;
            Added?.Invoke(this, new BinaryTreeItemAddedEventArgs<T>(item));
        }

        private void Add(Node<T> node, T item)
        {
            if (item.CompareTo(node.key) < 0)
            {
                if (node.left == null)
                    node.left = new Node<T>(item);
                else
                    Add(node.left, item);
            }
            else if (item.CompareTo(node.key) > 0)
            {
                if (node.right == null)
                    node.right = new Node<T>(item);
                else
                    Add(node.right, item);
            }
            else
            {
                throw new DuplicateItemException();
            }
        }

        public bool Contains(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return Contains(root, item);
        }

        private bool Contains(Node<T> node, T item)
        {
            if (node == null)
                return false;

            if (item.CompareTo(node.key) == 0)
                return true;
            else if (item.CompareTo(node.key) < 0)
                return Contains(node.left, item);
            else
                return Contains(node.right, item);
        }

        public bool Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (Remove(ref root, item))
            {
                Count--;
                Removed?.Invoke(this, new BinaryTreeItemRemovedEventArgs<T>(item));

                return true;
            }

            return false;
        }

        private bool Remove(ref Node<T> node, T item)
        {
            if (node == null)
                return false;

            if (item.CompareTo(node.key) < 0)
            {
                return Remove(ref node.left, item);
            }
            else if (item.CompareTo(node.key) > 0)
            {
                return Remove(ref node.right, item);
            }
            else if (node.left == null && node.right == null)
            {
                node = null;
            }
            else if (node.left == null)
            {
                node = node.right;
            }
            else if (node.right == null)
            {
                node = node.left;
            }
            else if (node.right.left == null)
            {
                node.right.left = node.left;
                node = node.right;
            }
            else
            {
                Node<T> curr = node.right;
                while (curr.left != null)
                {
                    curr = curr.left;
                }
                curr.left = node.left;
                node = node.right;
            }

            return true;
        }

        public void Clear()
        {
            root = null;
            Count = 0;
            Cleared?.Invoke(this, new BinaryTreeClearedEventArgs());
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Not enough elements after array index in the destination array.");

            int i = 0;
            foreach (T item in this)
            {
                array[i + arrayIndex] = item;
                i++;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => InOrder(root).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => InOrder(root).GetEnumerator();

        private IEnumerable<T> InOrder(Node<T> node)
        {
            if (node != null)
            {
                foreach (T key in InOrder(node.left))
                    yield return key;

                yield return node.key;

                foreach (T key in InOrder(node.right))
                    yield return key;

            }
        }

        public IEnumerable<T> Reverse() => Reverse(root);

        private IEnumerable<T> Reverse(Node<T> node)
        {
            if (node != null)
            {
                foreach (T key in Reverse(node.right))
                    yield return key;

                yield return node.key;

                foreach (T key in Reverse(node.left))
                    yield return key;
            }
        }

    }
}
