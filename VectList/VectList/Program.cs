using System;
using System.Collections.Generic;

namespace VectList
{
    class MyList<T>
    {
        public bool IsEmpty => size == 0;

        private int size;
        private T[] data;

        public MyList()
        {
            size = 0;
            data = new T[0];
        }

        public T this[int key]
        {
            get
            {
                if (key < 0 || key > size)
                    throw new IndexOutOfRangeException("Size is null");
                return data[key];
            }
            set
            {
                if (key < 0 || key > size) throw new ArgumentOutOfRangeException("Size is null");
                else data[key] = value;
            }
        }

        private void Resize(int newSi)
        {
            newSi = (int)Math.Pow(2, Math.Ceiling(Math.Log2(newSi)));
            if (newSi != data.Length)
                Array.Resize(ref data, newSi);
        }

        public void Add(T x)
        {
            Resize(++size);
            data[size - 1] = x;
        }

        public void Insert(int ind, T x)
        {
            if (ind < 0)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
            else if (ind > size)
            {
                Resize(++ind);
                data[ind] = x;
            }
            else
            {
                Resize(++size);
                for (int i = ind + 1; i < size - 2; i++)
                    data[i + 1] = data[i];
                data[ind] = x;
            }
        }

        public void RemoveAt(int ind)
        {
            if (ind < 0)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            if (size > 0)
            {
                for (int i = ind; i < size; i++)
                    data[i] = data[i + 1];
                Resize(--size);
            }
        }

        public T Last()
        {
            if (size > 0)
            {
                return data[size - 1];
            }
            else
            {
                return default(T);
            }
        }

        public T First()
        {
            if (size > 0)
            {
                return data[0];
            }
            else
            {
                return default(T);
            }
        }

        public void Clear()
        {
            size = 0;
            data = new T[0];
        }

        public int Count
        {
            get
            {
                return size;
            }
        }

        public bool Contains(T x) => IndexOf(x) != -1;
        public int IndexOf(T x)
        {
            for (int i = 0; i < size; i++)
                if (object.Equals(x, data[i]))
                    return i;

            return -1;
        }

        public IEnumerator<T> GetEnumerator() => data.GetEnumerator() as IEnumerator<T>;
        public void ForEach(Action<T> action)
        {
            for (int i = 0; i < size; i++)
                action(data[i]);
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
                if (match(data[i]))
                    return data[i];

            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
                if (match(data[i]))
                    return i;

            return -1;
        }
    }

    class Program
    {
        public static MyList<int> list = new MyList<int>();
        static void Main(string[] args)
        {
            list.Add(2);
            list.Add(2);
            list.Add(2);
            list.Add(2);
            list.Add(5);
            //Console.Write(list[0]);
            Console.WriteLine("Added 2 in the end");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} ", list[i]);
            }
            Console.WriteLine();

            list.Insert(3, 3);
            Console.WriteLine("Inserted 3 in 3d index");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} ", list[i]);
            }
            //Console.Write(list[6]);
            Console.WriteLine();

            list.RemoveAt(3);
            Console.WriteLine("Removed from 3d index");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} ", list[i]);
            }
            Console.ReadLine();
        }
    }
}
