using System;
using System.Collections.Generic;

namespace VectorList
{
    class MyList<T>
    {
        public int size;
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
                return data[key];
            }
            set
            {
                if (size == 0) throw new ArgumentOutOfRangeException("ArgumentOutOfRange");
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
            data[ind] = x;
            for (ind = ind + 1; ind < size; ind++) 
            {
                T temporal = data[ind + 1];
                data[ind + 1] = data[ind + 2];
                data[ind + 2] = data[ind + 1];
            }
            Array.Resize(ref data, ++size);
        }

        public T RemoveAt(int ind)
        {
            if (size != 0)
            {
                var temp = data[ind];
                Array.Resize(ref data, --size);
                return temp;
            }
            return default;
        }

        public T Last()
        {
            if (size != 0)
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
            if (size != 0)
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
        int[] mas;
        static MyList<List<int>> list = new MyList<List<int>>();

        public void Main(string[] args)
        {
            mas = new int[7];
        }
    }
}
