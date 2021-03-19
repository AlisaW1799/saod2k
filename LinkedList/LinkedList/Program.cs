using System;
using System.Collections.Generic;

namespace LinkedList
{
    class Program
    {
        public class Node<T>
        {
            public T data;
            public Node<T> Next { get; set; }

            public Node(T x)
            {
                this.data = x;
            }
        }

        public class SLList<T>
        {
            public int size { private set; get; }
            private Node<T> head;

            public T First => size > 0 ? head.data : default;
            public T Last => size > 0 ? this[size - 1] : default;

            public void AddLast(T x)
            {
                var newNode = new Node<T>(x);

                if (size > 0)
                    GetValue(size - 1).Next = newNode;
                else
                    head = newNode;

                size++;
            }

            public void AddFirst(T x)
            {
                var newNode = new Node<T>(x);

                if (size > 0)
                    newNode.Next = head;
                head = newNode;

                size++;
            }

            public void Insert(int ind, T x)
            {
                if (ind == size)
                    AddLast(x);
                else if (ind == 0)
                    AddFirst(x);
                else
                {
                    var next = GetValue(ind);
                    var previous = GetValue(ind - 1);
                    var newNode = new Node<T>(x);
                    newNode.Next = next;
                    previous.Next = newNode;
                    size++;
                }
            }

            public void RemoveAt(int ind)
            {
                var newNode = GetValue(ind);
                if (size == 1)
                    head = null;
                else if (head == newNode)
                    head = newNode.Next;
                else
                    GetValue(ind - 1).Next = newNode.Next;
                size--;
            }

            public void Clear()
            {
                head = null;
                size = 0;
            }

            public int IndexOf(T x)
            {
                var curr = head;
                if (object.Equals(curr.data, x))
                    return 0;

                for (var i = 1; i < size; i++)
                {
                    curr = curr.Next;
                    if (object.Equals(curr.data, x))
                        return i;
                }

                return -1;
            }

            public T this[int ind]
            {
                get
                {
                    if (ind < 0 || ind >= size)
                        throw new IndexOutOfRangeException("Index is out of range: index = " + ind + "; size = " + size);
                    return GetValue(ind).data;
                }
                set
                {
                    if (ind < 0 || ind >= size)
                        throw new IndexOutOfRangeException("Index is out of range: index = " + ind + "; size = " + size);
                    GetValue(ind).data = value;
                }
            }

            public Node<T> GetValue(int ind)
            {
                var curr = head;
                for (var i = 0; i < ind; i++)
                {
                    curr = curr.Next;
                }
                return curr;
            }

            public T[] ToArray()
            {
                var array = new T[size];
                if (size == 0)
                    return array;

                var temp = head;
                array[0] = temp.data;

                for (var i = 1; i < size; i++)
                {
                    temp = temp.Next;
                    array[i] = temp.data;
                }
                return array;
            }
            public IEnumerator<T> GetEnumerator() =>
                ToArray().GetEnumerator() as IEnumerator<T>;
        }



        static void Main(string[] args)
        {
            var list = new SLList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            for (int i = 0; i < list.size; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();

            list.RemoveAt(2);
            for (int i = 0; i < list.size; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();

            list.AddFirst(5);
            for (int i = 0; i < list.size; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine(list.IndexOf(2) + "\n");

            Console.WriteLine("null");


            var list1 = new SLList<int>();
            list1.AddLast(1);
            list1.AddLast(2);
            list1.AddLast(3);
            Console.WriteLine(list1.IndexOf(2) + "\n");

            Console.ReadLine();
        }
    }

    public static class MyExt
    {
        public static int IndexOF<T>(this LinkedList<T> l, T item)
        {
            int rez = -1, c = 0;
            foreach (var i in l)
            {
                if (i.Equals(item))
                    return c;
                c++;
            }
            return rez;
        }
    }
}