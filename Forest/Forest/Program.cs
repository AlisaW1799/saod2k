using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private TreeNode<T> _root;
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

        public TreeNode<T> Find(T value)
        {
            return Find(value, root);
        }
        private TreeNode<T> Find(T value, TreeNode<T> subroot)
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

        public void Print()
        {
            _print(root);
        }

        private void _print(TreeNode<T> node)
        {
            if (node == null) 
                return;
            _print(node.left);
            Console.WriteLine(node.Value);
            _print(node.right);
        }

        private void store(List<TreeNode<T>> n, TreeNode<T> p)
        {
            if (p == null)
            {
                return;
            }
            store(n, p.left);
            n.Add(p);
            store(n, p.right);
        }

        public void ReorderToIdeal()
        {
            root = MakeIdeal(root);
        }
        private TreeNode<T> MakeIdeal(TreeNode<T> p)
        {
            var nodes = new List<TreeNode<T>>();
            store(nodes, p);
            return BuildIdeal(nodes, 0, nodes.Count - 1);
        }

        private TreeNode<T> BuildIdeal(List<TreeNode<T>> n, int a, int b)
        {
            if (a > b)
                return null;

            int m = (a + b) / 2;
            var p = n[m];
            p.left = BuildIdeal(n, a, m - 1);
            p.right = BuildIdeal(n, m + 1, b);
            return p;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Stree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);
            tree.Add(5);
            tree.Add(6);
            tree.Add(7);

            Console.WriteLine();
            //var rnd = new System.Random(1);
            //var init = Enumerable.Range(0, 10_000_000).OrderBy(x => rnd.Next()).ToArray();
            //foreach (var i in init)
            //{
            //    tree.Add(i);
            //}

            tree.Print();

            Console.WriteLine();
            Console.WriteLine(tree.GetHeight());
            tree.ReorderToIdeal();
            Console.WriteLine(tree.GetHeight());

            //Console.WriteLine(string.Join(", ", tree.SymmetricBypass.Select(num => num.ToString())));
        }
    }
}