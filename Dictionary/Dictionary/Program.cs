using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Dictionary
{

    public class AVLTree
    {
        public class Node
        {
            public Node left;
            public Node right;

            public int x;
            public int height;

            public Node(int x)
            {
                this.x = x;
                height = 1;
            }
        }

        public Node root;

        private int height(Node p)
        {
            return p == null ? 0 : p.height;
        }

        private int balance(Node p)
        {
            return p == null ? 0 : height(p.left) - height(p.right);
        }

        private void update_height(Node p)
        {
            p.height = 1 + Math.Max(height(p.left), height(p.right));
        }

        private Node rightRotate(Node y)
        {
            var x = y.left;
            var t = x.right;

            x.right = y;
            y.left = t;

            update_height(y);
            update_height(x);

            return x;
        }

        private Node leftRotate(Node x)
        {
            var y = x.right;
            var t = y.left;

            y.left = x;
            x.right = t;

            update_height(x);
            update_height(y);

            return y;
        }

        public void Add(int x)
        {
            root = insert(root, x);
        }

        private Node insert(Node p, int x)
        {
            if (p == null)
                return new Node(x);

            if (x < p.x)
                p.left = insert(p.left, x);
            else
                p.right = insert(p.right, x);

            update_height(p);
            int b = balance(p);

            if (b > 1 && x < p.left.x)
                return rightRotate(p);
            if (b < -1 && x > p.right.x)
                return leftRotate(p);
            if (b > 1 && x > p.left.x)
            {
                p.left = leftRotate(p.left);
                return rightRotate(p);
            }
            if (b < -1 && x < p.right.x)
            {
                p.right = rightRotate(p.right);
                return leftRotate(p);
            }
            return p;
        }

        private KeyValuePair<int, string> ToStringHelper(Node n)
        {
            if (n == null)
                return new KeyValuePair<int, string>(1, "\n");

            var left = ToStringHelper(n.left);
            var right = ToStringHelper(n.right);

            var obj = n.x.ToString();
            var str = new System.Text.StringBuilder();

            str.Append(' ', left.Key - 1);
            str.Append(obj);
            str.Append(' ', right.Key - 1);
            str.Append('\n');

            var i = 0;
            while (i * left.Key < left.Value.Length && i * right.Key < right.Value.Length)
            {
                str.Append(left.Value, i * left.Key, left.Key - 1);
                str.Append(' ', obj.Length);
                str.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }
            while (i * left.Key < left.Value.Length)
            {
                str.Append(left.Value, i * left.Key, left.Key - 1);
                str.Append(' ', obj.Length + right.Key - 1);
                str.Append('\n');
                ++i;
            }
            while (i * right.Key < right.Value.Length)
            {
                str.Append(' ', left.Key + obj.Length - 1);
                str.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }
            return new KeyValuePair<int, string>(left.Key + obj.Length + right.Key - 1, str.ToString());
        }
        public override string ToString()
        {
            return ToStringHelper(root).Value;
        }
    }

    public class Dict<TKey, TValue>
    {
        public Dict(int capacity)
        {
            comp = EqualityComparer<TKey>.Default;
            buckets = new int[capacity];
            entries = new Entry[capacity];
            for (int i = 0; i < capacity; i++)
            {
                buckets[i] = -1;
            }
            freeList = -1;
        }

        private void Resize()
        {
            Resize(count * 2);
        }
        private void Resize(int newSize)
        {
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++)
                newBuckets[i] = -1;
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(entries, 0, newEntries, 0, count);

            for (int i = 0; i < count; i++)
            {
                if (newEntries[i].HashCode >= 0)
                {
                    int bucket = newEntries[i].HashCode % newSize;
                    newEntries[i].next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }
            buckets = newBuckets;
            entries = newEntries;
        }

        public void Add(TKey key, TValue value)
        {
            int hashCode = comp.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % buckets.Length;

            for (int i = buckets[targetBucket]; i >= 0; i = entries[i].next)
            {
                if (entries[i].HashCode == hashCode && comp.Equals(entries[i].key, key))
                {
                    entries[i].value = value;
                    return;
                }
            }

            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = entries[index].next;
                freeCount--;
            }
            else
            {
                if (count == entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % buckets.Length;
                }
                index = count;
                count++;
            }
            entries[index].HashCode = hashCode;
            entries[index].next = buckets[targetBucket];
            entries[index].key = key;
            entries[index].value = value;
            buckets[targetBucket] = index;
        }

        private int FindEntry(TKey key)
        {
            if (buckets != null)
            {
                int hashCode = comp.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].HashCode == hashCode && comp.Equals(entries[i].key, key))
                        return i;
                }
            }
            return -1;
        }
        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0)
                {
                    return entries[i].value;
                }
                return default(TValue);
            }
            set
            {
                Add(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return FindEntry(key) >= 0;
        }

        public void Print()
        {
            Console.WriteLine("Index\tBuckets\t\tEntries");
            for (int i = 0; i < buckets.Length; i++)
            {
                Console.Write(i + "\t" + buckets[i]);
                Console.Write("\t\tKey: " + entries[i].key);
                Console.WriteLine(entries[i].next <= 0 ? "" : "->" + entries[i].next);
            }
        }

        public struct Entry
        {
            public int HashCode;
            public int next;
            public TKey key;
            public TValue value;
        }
        private int[] buckets;
        private Entry[] entries;
        private int freeList;
        private int freeCount;
        private int count;
        private IEqualityComparer<TKey> comp;
    }

    class Program
    {

        static void Main(string[] args)
        {
            string input_text = System.IO.File.ReadAllText(@"big.txt");

            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            long elapsedMs;

            string text = input_text;
            //string text = " aa bb c aa b c aa b ";
            var str = new StringBuilder();
            var d = new /*SortedDictionary*/Dict<string, int>(2);
            //var d = new AVLTree<string, int>();
            foreach (var c in text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    str.Append(c);
                }
                else if (str.Length > 0)
                {
                    try { ++d[str.ToString()]; }
                    catch (KeyNotFoundException) { d[str.ToString()] = 1; }
                    str.Clear();
                }

            }
            Console.WriteLine(d["the"]);



            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            System.Console.WriteLine("time: " + elapsedMs);

        }
    }
}