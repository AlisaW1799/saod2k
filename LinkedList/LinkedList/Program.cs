using System;
using System.Collections.Generic;

namespace LinkedList
{
    class Program
    {
        public class Node
        {
            public int data;
            public Node next;

            public Node(int x)
            {
                this.data = x;
                this.next = null;
            }
        }

        public class SLList
        {
            public Node head;
            public Node tail;
            int size = 0;

            public SLList()
            {
                head = null;
                tail = head;
                size = 0;
            }

            public void AddLast(int x)
            {
                ++size;
                var newNode = new Node(x);

                if (head == null)
                {
                    head = newNode;
                    tail = head;
                }
                else
                {
                    tail.next = newNode;
                    tail = newNode;
                }
            }

            public void AddFirst(int x)
            {
                var newNode = new Node(x);
                ++size;
                if (head == null)
                {

                }
                else
                {
                    newNode.next = head;
                    head=
                }
            }

            public int this[int ind]
            {
                get
                {
                    return GetValue(ind);
                }
                set
                {
                    //написать
                }
            }
            public int GetValue(int p)
            {
                var curr = head;
                for (int i = 0; i < p && curr != null; i++)
                {
                    curr = curr.next;
                }
                return curr.data;
            }

            public IEnumerable(int Enumerate)
            {
                for (int i = 0; i < size; i++)
                {
                    return yield GetValue();
                }
            }
        }
        static void Main(string[] args)
        {
            var list = new SLList();
        }
    }
}
