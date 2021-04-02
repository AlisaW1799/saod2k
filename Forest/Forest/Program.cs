using System;
using System.Collections.Generic;
using System.Linq;

namespace Forest
{
    public class Node
    {
        public int data;
        public Node left { get; set; }
        public Node right { get; set; }

        public Node(int x)
        {
            this.data = x;
        }
    }

    class Stree
    {
        public int size { private set; get; }
        Node root;
        public void Insert(int x)
        {
            if (root == null)
                root = new Node(x);
            else
                Insert(x, root);
        }
        private void Insert(int x, Node p)
        {
            if (x < p.data)
            {
                if (p.left == null)
                    p.left = new Node(x);
                else
                    Insert(x, p.left);
            }
            else if(x > p.data)
            {
                if (p.right == null)
                    p.right = new Node(x);
                else
                    Insert(x, p.right);
            }
            size++;
        }

        //public void Clear()
        //{
        //    left = null;
        //    right = null;
        //    root = null;
        //    size = 0;
        //}

        public int GetHeight()
        {
            return geth(root);
        }
        int geth(Node p)
        {
            if (p == null)
            {
                return 0;
            }
            return 1 + Math.Min(geth(p.left), geth(p.right));
        }

        public void print()
        {
            print(root, 0);
        }
        private void print(Node p, int h)
        {
            int x = 0;
            if (p.right != null)
                print(p.right, h + 1);
            for (int i = 0; i < h; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(p.data);
            if (p.left != null)
            {
                print(p.left, h + 1);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Stree();
            //tree.Insert(3);
            //tree.Insert(2);
            //tree.Insert(1);
            //tree.print();

            Console.WriteLine();
            Console.WriteLine(tree.GetHeight());

            var rnd = new System.Random(1);
            var init = Enumerable.Range(0, 25).OrderBy(x => rnd.Next()).ToArray();
            foreach (var i in init)
            {
                tree.Insert(i);
            }

            for (int i = 0; i < 3; i++)
            {

            }

            //List <int> list = new List<int>();
            //for (int i = 0; i < n; i++)
            //{

            //}
        }
    }
}