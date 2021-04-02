using System;

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
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
