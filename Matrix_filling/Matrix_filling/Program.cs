using System;
using System.Collections.Generic;

namespace Matrix_filling
{
    class MyStack<T>
    {
        private T[] data;
        public int size;

        public MyStack()
        {
            size = 0;
            data = new T[0];
        }

        public void Push(T x)
        {
            Array.Resize(ref data, ++size);
            data[size - 1] = x;
        }

        public T Pop()
        {
            if (size != 0)
            {
                var temp = data[size - 1];
                Array.Resize(ref data, --size); 
                return temp;
            }
            return default;
        }

        public T Peek()
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

        public bool isEmpty()
        {
            return size == 0;
        }

        public void Clear()
        {
            size = 0;
            data = new T[0];
        }
    }

    public class Program
    {
        static int[,] mat;
        static MyStack<KeyValuePair<int, int>> stack = new MyStack<KeyValuePair<int, int>>();
        const int p = 10;

        static void Main(string[] args)
        {
            mat = new int[p, p];
            
            Console.WriteLine("До:");
            //Outp(mat);
            Create(p);  
            Counter(mat);

            FillRecursion(2, 2, mat);
            Console.WriteLine("После:");
            Counter(mat);
            //Outp(mat);

            Console.WriteLine("Stack:");
            ImStack(2, 2, mat);
            Counter(mat);
        }

        public static void Create(int p)
        {
            mat = new int[p, p];
            Random r = new Random();
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    if (r.Next(0, 4) == 1)
                    {
                        mat[i, j] = 1;
                    }
                }
            }
        }

        public static void FillRecursion(int x, int y, int[,] m)
        {
            int f = 0;
            int c = 2;
            if (x >= 0 && y >= 0 && x < p && y < p && m[x, y] == f) 
            {
                m[x, y] = c;
                FillRecursion(x + 1, y, m);
                FillRecursion(x, y + 1, m);
                FillRecursion(x - 1, y, m);
                FillRecursion(x, y - 1, m);
            }
        }

        static void Counter(int[,] mat)
        {
            int c1 = 0;
            int c2 = 0;
            int c0 = 0;
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(0); j++)
                {
                    if (mat[i, j] == 1)
                    {
                        c1++;
                    }
                    if (mat[i, j] == 2)
                    {
                        c2++;
                    }
                    if (mat[i, j] == 0)
                    {
                        c0++;
                    }
                }
            }
            Console.WriteLine("1 - " + c1);
            Console.WriteLine("2 - " + c2);
            Console.WriteLine("0 - " + c0);
            Console.WriteLine();
        }

        public static void ImStack(int x, int y, int[,] max)
        {
            const int c = 2;
            stack.Push(new KeyValuePair<int, int>(x, y));
            while (stack.size > 0)
            {
                var st = stack.Pop();
                if (st.Key >= 0 && st.Value >= 0 && st.Key < max.GetLength(0) && st.Value < max.GetLength(0) && max[st.Key, st.Value] == 0)
                {
                    max[st.Key, st.Value] = c;
                    stack.Push(new KeyValuePair<int, int>(st.Key, st.Value - 1));
                    stack.Push(new KeyValuePair<int, int>(st.Key - 1, st.Value));
                    stack.Push(new KeyValuePair<int, int>(st.Key, st.Value + 1));
                    stack.Push(new KeyValuePair<int, int>(st.Key + 1, st.Value));
                }
            }
        }

        public static void Outp(int[,] mat)
        {
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    Console.Write("{0}\t", mat[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
