using System;
using System.Collections.Generic;

namespace LinkedList
{
    public class ListNode<T>
    {
        public T data;
        public ListNode<T> Next { get; set; }
        public ListNode<T> Prev { get; set; }

        public ListNode(T value)
        {
            data = value;
        }
    }

    public class SLList<T>
    {
        public int size { private set; get; }
        private ListNode<T> head;
        private ListNode<T> tail;

        public T First => size > 0 ? head.data : default;
        public T Last => size > 0 ? tail.data : default;

        public void AddLast(T x)
        {
            var node = new ListNode<T>(x);
            if (size > 0)
            {
                node.Prev = tail;
                tail.Next = node;
            }
            tail = node;

            if (head == null)
                head = tail;
            size++;
        }

        public void AddFirst(T x)
        {
            var node = new ListNode<T>(x);
            if (size > 0)
            {
                node.Next = head;
                head.Prev = node;
            }
            head = node;
            if (tail == null) tail = head;
            size++;
        }

        public void Insert(int ind, T x)
        {
            if (ind == size) AddLast(x);
            else if (ind == 0) AddFirst(x);
            else
            {
                var next = GetValue(ind);
                var prev = GetValue(ind - 1);
                var node = new ListNode<T>(x);
                node.Next = next;
                node.Prev = prev;
                next.Prev = node;
                prev.Next = node;
                size++;
            }
        }

        public void RemoveAt(int ind)
        {
            var node = GetValue(ind);
            if (size == 1)
            {
                head = null;
                tail = null;
            }
            else if (head == node)
                head = node.Next;
            else if (tail == node)
                tail = node.Prev;
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
            size--;
        }

        public void Clear()
        {
            head = null;
            tail = null;
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

        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= size)
                    throw new IndexOutOfRangeException("Index is out of range: index = " + i + "; size = " + size);
                return GetValue(i).data;
            }
            set
            {
                if (i < 0 || i >= size)
                    throw new IndexOutOfRangeException("Index is out of range: index = " + i + "; size = " + size);
                GetValue(i).data = value;
            }
        }

        private ListNode<T> GetValue(int ind)
        {
            var curr = head;
            for (var i = 0; i < ind; i++) 
                curr = curr.Next;
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

    class Program
    {
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