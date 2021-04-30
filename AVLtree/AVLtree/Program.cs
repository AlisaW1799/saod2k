using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AVLtree
{
    public class TreeNode<T> where T : IComparable
    {
        public TreeNode<T> left, right;
        public int height;
        public T Value;
        public TreeNode(T value)
        {
            Value = value;
            height = 1;
        }
    }

    public class AVLtree<T> where T : IComparable
    {
        public TreeNode<T> root;

        private int height(TreeNode<T> p)
        {
            return p == null ? 0 : p.height;
        }
        private int balance(TreeNode<T> p)
        {
            return p == null ? 0 : height(p.left) - height(p.right);
        }

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

        //public T[] SymmetricBypass
        //{
        //    get
        //    {
        //        sym.Clear();
        //        Symmetric(root);
        //        return sym.ToArray();
        //    }
        //}
        //private List<T> sym = new List<T>();
        //private void Symmetric(TreeNode<T> node)
        //{
        //    if (node == null) return;
        //    Symmetric(node.left);
        //    sym.Add(node.Value);
        //    Symmetric(node.right);
        //}

        //right rotation
        public TreeNode<T> Rrot(TreeNode<T> r)
        {
            var p = r.left;
            r.left = p.right;
            p.right = r;
            return p;
        }

        //left rotation
        public TreeNode<T> Lrot(TreeNode<T> r)
        {
            var p = r.right;
            r.right = p.left;
            p.left = r;
            return p;
        }

        //big right rotation
        public TreeNode<T> BRrot(TreeNode<T> r)
        {
            var p = r.left;
            r.left = Lrot(p);
            return Rrot(r);
        }

        //big left rotation
        public TreeNode<T> BLrot(TreeNode<T> r)
        {
            var p = r.right;
            r.right = Rrot(p);
            return Lrot(r);
        }

        private KeyValuePair<int, string> ToStringHelper(TreeNode<T> n)
        {
            if (n == null)
                return new KeyValuePair<int, string>(1, "\n");

            var left = ToStringHelper(n.left);
            var right = ToStringHelper(n.right);

            var objString = n.Value.ToString();
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(' ', left.Key - 1);
            stringBuilder.Append(objString);
            stringBuilder.Append(' ', right.Key - 1);
            stringBuilder.Append('\n');

            var i = 0;
            while (i * left.Key < left.Value.Length && i * right.Key < right.Value.Length)
            {
                stringBuilder.Append(left.Value, i * left.Key, left.Key - 1);
                stringBuilder.Append(' ', objString.Length);
                stringBuilder.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }

            while (i * left.Key < left.Value.Length)
            {
                stringBuilder.Append(left.Value, i * left.Key, left.Key - 1);
                stringBuilder.Append(' ', objString.Length + right.Key - 1);
                stringBuilder.Append('\n');

                ++i;
            }

            while (i * right.Key < right.Value.Length)
            {
                stringBuilder.Append(' ', left.Key + objString.Length - 1);
                stringBuilder.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }
            return new KeyValuePair<int, string>(left.Key + objString.Length + right.Key - 1, stringBuilder.ToString());
        }
        public string Print()
        {
            var res = ToStringHelper(root).Value;
            return res;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var list = new AVLtree<int>();

            //list.Add(5);
            //list.Add(3);
            //list.Add(2);
            //list.Add(4);
            //list.Add(7);

            //Console.WriteLine(list.Print());
            ////Console.WriteLine();
            ////Console.WriteLine(list.GetHeight);
            //list.root = list.Rrot(list.root);                                         //right rotation
            //Console.WriteLine(list.Print());

            //list.root = list.Lrot(list.root);                                         //left rotation
            //Console.WriteLine(list.Print());

            ////Console.WriteLine(string.Join(", ", list.SymmetricBypass.Select(num => num.ToString())));


            //list.Add(5);
            //list.Add(1);
            //list.Add(0);
            //list.Add(3);
            //list.Add(2);
            //list.Add(4);
            //list.Add(6);

            //Console.WriteLine(list.Print());

            //list.root = list.BRrot(list.root);
            //Console.WriteLine(list.Print());

            list.Add(4);
            list.Add(1);
            list.Add(9);
            list.Add(6);
            list.Add(10);
            list.Add(7);
            list.Add(5);

            Console.WriteLine(list.Print());

            list.root = list.BLrot(list.root);
            Console.WriteLine(list.Print());
        }
    }
}

