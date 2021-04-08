using System;
using System.Collections.Generic;
using System.Linq;

namespace Forest
{
    public class TreeNode<T> where T : IComparable
    {
        public TreeNode<T> left, right;
        public T Value;
        public TreeNode(T value)
        {
            Value = value;
        }
    }

    public class Stree<T> where T : IComparable
    {
        TreeNode<T> root;

        public void Add(T value)
        {
            TreeNode<T> node = new TreeNode<T>(value);
            if (root == null)
                root = node;
            else
                Add(node, root);
        }
        private void Add(TreeNode<T> node, TreeNode<T> subroot)
        {
            if (subroot.Value.CompareTo(node.Value) <= 0) 
            {
                if (subroot.right == null)
                    subroot.right = node;
                else
                    Add(node, subroot.right);
            }
            else
            {
                if (subroot.left == null)
                    subroot.left = node;
                else
                    Add(node, subroot.left);
            }
        }

        public void Clear()
        {
            root = null;
        }

        public int Size()
        {
            return Size(root);
        }
        private int Size(TreeNode<T> subroot)
        {
            if (subroot == null)
                return 0;
            return 1 + Size(subroot.left) + Size(subroot.right);
        }

        public T MaxValueIter()
        {
            return GetMaxOrMinIter(new Func<TreeNode<T>, TreeNode<T>>(node => node.right));
        }
        public T MinValueIter()
        {
            return GetMaxOrMinIter(new Func<TreeNode<T>, TreeNode<T>>(node => node.left));
        }
        private T GetMaxOrMinIter(Func<TreeNode<T>, TreeNode<T>> next)
        {
            if (root == null)
                return default;
            var t = root;
            while (next(t) != null)
                t = next(t);
            return t.Value;
        }

        public T MaxValueRec()
        {
            return GetMaxOrMinRec(new Func<TreeNode<T>, TreeNode<T>>(node => node.right), root);
        }
        public T MinValueRec()
        {
            return GetMaxOrMinRec(new Func<TreeNode<T>, TreeNode<T>>(node => node.left), root);
        }
        private T GetMaxOrMinRec(Func<TreeNode<T>, TreeNode<T>> next, TreeNode<T> node)
        {
            if (node == null)
                return default;
            if (next(node) == null)
                return node.Value;
            return GetMaxOrMinRec(next, next(node));
        }

        public TreeNode<T> Find (T value)
        {
            return Find(value, root);
        }
        private TreeNode<T> Find (T value, TreeNode<T> subroot)
        {
            if (subroot == null)
                return null;
            if (value.CompareTo(subroot.Value) == 0)
                return subroot;
            else if (value.CompareTo(subroot.Value) < 0)
                return Find(value, subroot.left);
            else
                return Find(value, subroot.right);
        }

        public bool Contains(T value)
        {
            return Contains(value, root);
        }
        private bool Contains(T value, TreeNode<T> node)
        {
            if (node == null)
                return false;
            if (object.Equals(node.Value, value))
                return true;
            if (value.CompareTo(node.Value) > 0)
                return Contains(value, node.right);
            else 
                return Contains(value, node.left);
        }

        public int LeafCount()
        {
            return LeafCount(root);
        }
        private int LeafCount(TreeNode<T> subroot)
        {
            if (subroot == null)
                return 0;
            if (subroot.left == null && subroot.right == null)
                return 1;
            return LeafCount(subroot.left) + LeafCount(subroot.right);
        }

        public int GetHeight()
        {
            return GetHeight(root);
        }
        private int GetHeight(TreeNode<T> subroot)
        {
            if (subroot == null)
                return 0;
            return 1 + Math.Max(GetHeight(subroot.left), GetHeight(subroot.right));
        }

        public T[] SymmetricBypass
        {
            get
            {
                sym.Clear();
                Symmetric(root);
                return sym.ToArray();
            }
        }
        private List<T> sym = new List<T>();
        private void Symmetric(TreeNode<T> node)
        {
            if (node == null) return;
            Symmetric(node.left);
            sym.Add(node.Value);
            Symmetric(node.right);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var list = new Stree<int>();
            list.Add(5);
            list.Add(8);
            list.Add(3);
            list.Add(1);
            list.Add(4);
            list.Add(6);
            list.Add(9);

            Console.WriteLine();
            //Console.WriteLine(list.GetHeight);
            Console.WriteLine(string.Join(", ", list.SymmetricBypass.Select(num => num.ToString())));
        }
    }
}
