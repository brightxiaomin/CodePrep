using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class SimpleHeap
    {
        private static readonly int DEFAULT_CAPACITY = 11;
        private  int used;
        private  int[] store;

        public SimpleHeap() : this(DEFAULT_CAPACITY)
        {

        }

        public SimpleHeap(int cap)
        {
            used = 0;
            store = new int[cap];
        }

        public bool IsEmpty()
        {
            return used == 0;
        }

        public int GetSize()
        {
            return used;
        }

        public int Peek()
        {
            if (used == 0)
                throw new IndexOutOfRangeException();

            return store[0];
        }

        public int Pop()
        {
            if (used == 0)
                throw new IndexOutOfRangeException();

            var result = store[0];
            store[0] = store[used - 1];
            used--;

            Sink();
            return result;
        }

        public void Add(int val)
        {
            if (used == store.Length)
                Resize();

            store[used] = val;
            used++;

            BubbleUp();
        }

        private void BubbleUp()
        {
            int index = used - 1;
            while (index > 0 && store[index] < store[(index - 1) / 2])
            {
                Swap(store, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        private void Sink()
        {
            int index = 0;
            while (true)
            {
                int min = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                if (left < used && store[min] > store[left]) min = left;
                if (right < used && store[min] > store[right]) min = right;
                if (min == index) return; // NO swap

                Swap(store, min, index);
                index = min;
            }
        }

        public void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        void Resize()
        {
            int[] temp = new int[2 * store.Length];
            store.CopyTo(temp, 0);
            store = temp;
        }
    }


    public class MedianFinder
    {
        private Heap left;
        private Heap right;
        /** initialize your data structure here. */
        public MedianFinder()
        {
            left = new Heap(1000, false);  //max for left
            right = new Heap(1000, true); //true for right
        }

        public void AddNum(int num)
        {
            if (left.IsEmpty() || num <= left.Peek()) left.Add(num);
            else right.Add(num);
            //keep balance
            if (left.Count() - right.Count() > 1) right.Add(left.Pop());
            if (right.Count() - left.Count() > 1) left.Add(right.Pop());
        }

        public double FindMedian()
        {
            if (left.Count() == right.Count())
            {
                return (double)(left.Peek() + right.Peek()) / 2;
            }
            else if (left.Count() > right.Count())
            {
                return left.Peek();
            }
            else
            {
                return right.Peek();
            }
        }
    }
    public class Heap
    {
        private int used;
        private int[] store;
        private bool isMin;

        public Heap(int cap, bool min)
        {
            used = 0;
            store = new int[cap];
            isMin = min;
        }

        public bool IsEmpty()
        {
            return used == 0;
        }

        public int Count()
        {
            return used;
        }

        public int Peek()
        {
            if (used == 0)
                throw new IndexOutOfRangeException();

            return store[0];
        }

        public int Pop()
        {
            if (used == 0)
                throw new IndexOutOfRangeException();

            var result = store[0];
            store[0] = store[used - 1];
            used--;

            Sink();
            return result;
        }

        public void Add(int val)
        {
            if (used == store.Length)
                Resize();

            store[used] = val;
            used++;

            BubbleUp();
        }

        private void BubbleUp()
        {
            int index = used - 1;
            while (index > 0 && Compare(store[index], store[(index - 1) / 2]))
            {
                Swap(store, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        private void Sink()
        {
            int index = 0;
            while (true)
            {
                int cur = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                if (left < used && Compare(store[left], store[cur])) cur = left;
                if (right < used && Compare(store[right], store[cur])) cur = right;

                if (cur == index) return; // NO swap

                Swap(store, cur, index);
                index = cur;
            }
        }
        void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        void Resize()
        {
            int[] temp = new int[2 * store.Length];
            store.CopyTo(temp, 0);
            store = temp;
        }

        bool Compare(int a, int b)
        {
            return isMin ? a < b : a > b;
        }
    }
}
