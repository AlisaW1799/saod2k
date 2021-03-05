using System;
using System.Collections.Generic;

namespace Lambda
{
    public class KeyVal
    {
        public int x;
        public int y;

        public KeyVal(int a, int b)
        {
            x = a;
            y = b;
        }
    }
    public class MyComp : IComparer<KeyVal>
    {
        public int Compare(KeyVal l, KeyVal r)
        {
            return l.y < r.y ? -1 : 1;
        }
    }

    delegate int f(int x);
    delegate int g(int a, int b);

    class Program
    {
        static void Main(string[] args)
        {
            //var m = new KeyVal[5];
            //m[0] = new KeyVal(2, 5);
            //m[1] = new KeyVal(4, 4);
            //m[2] = new KeyVal(6, 3);
            //m[3] = new KeyVal(8, 2);
            //m[4] = new KeyVal(10, 1);
            //Array.Sort(m, new MyComp());
            //foreach(var l in m)
            //{
            //    Console.WriteLine(l.y);
            //}


            //f sq = x => x * x;
            //Console.WriteLine(sq(5));


            //g sum = (x, y) => x + y;
            //Console.WriteLine(sum(4, 6));


            //System.Action<int> aa = null;
            //aa = x => Console.Write(x);
            //aa(5);


            //System.Func<int, int, int> ff = null;
            //ff = (a, b) =>
            //  {
            //      if (a < b) { return a + b; }
            //      else { return a * a + b * b; }
            //  };
            //Console.Write(ff(5, 5));


            //Func<int, int> fib = null;
            //fib = x => x > 1 ? fib(x - 1) + fib(x - 2) : x;
            //Console.Write(fib(10));

            //var m = new KeyVal[5];
            //m[0] = new KeyVal(2, 5);
            //m[1] = new KeyVal(4, 4);
            //m[2] = new KeyVal(6, 3);
            //m[3] = new KeyVal(8, 2);
            //m[4] = new KeyVal(10, 1);
            //Array.Sort(m, (a, b) => a.y.CompareTo(b.y));
            //foreach (var l in m)
            //{
            //    Console.WriteLine(l.y);
            //}


            var lst = new List<int> { 0, 1, 2, 3, 4 };
            Console.WriteLine(lst.FindIndex(x => x % 3 == 0));
            lst.ForEach(x => Console.WriteLine(x));
        }
    }
}
