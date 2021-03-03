using System;

namespace VectList
{
    class MyList<T>
    {
        public int size;
        private T[] data;
        public int counter;

        public MyList()
        {
            size = 0;
            data = new T[0];
        }

        public T this[int key]
        {
            get
            {
                return data[key];
            }
            set
            {
                if (size == 0) throw new ArgumentOutOfRangeException("Size is null");
                else data[key] = value;
            }
        }

        public void Add(T x)
        {
            Array.Resize(ref data, ++size);
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
                size += ind;
                Array.Resize(ref data, size);
                data[ind] = x;
            }
            else if (ind == size)
            {
                data[ind] = x;
                Array.Resize(ref data, size * 2);
            }

            else if (ind < size)
            {
                for (int i = ind; i < size - 1; i++) 
                {
                    data[i + 1] = data[i];
                }
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
                for (int i = ind; i < size - 1; i++) 
                {
                    data[i] = data[i + 1];
                }
                Array.Resize(ref data, --size);
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
    }

    class Program
    {
        public static MyList<int> list = new MyList<int>();
        static void Main(string[] args)
        {
            list.Add(2);
            //Console.Write(list[0]);
            Console.WriteLine("Added 2 in the end");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} ", list[i]);
            }
            Console.WriteLine();

            list.Insert(6, 3);
            Console.WriteLine("Added 3 as 6th");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} ", list[i]);
            }
            //Console.Write(list[6]);
            Console.WriteLine();

            list.RemoveAt(4);
            Console.WriteLine("Removed from 4th");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write("{0} ", list[i]);
            }
            Console.ReadLine();
        }
    }
}
