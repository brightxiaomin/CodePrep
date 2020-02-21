using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    //It passed the test Yes
    public class MedianFinderHeap
    {
        private MaxHeap left;
        private MinHeap right;
        /** initialize your data structure here. */
        public MedianFinderHeap()
        {
            left = new MaxHeap(1000);
            right = new MinHeap(1000);
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

    public class MinHeap
    {
        private int used;
        private int[] store;

        public MinHeap(int cap)
        {
            used = 0;
            store = new int[cap];
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
    }

    public class MaxHeap
    {
        private int used;
        private int[] store;

        public MaxHeap(int cap)
        {
            used = 0;
            store = new int[cap];
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
            while (index > 0 && store[index] > store[(index - 1) / 2])
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
                int max = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                if (left < used && store[max] < store[left]) max = left;
                if (right < used && store[max] < store[right]) max = right;
                if (max == index) return; // NO swap

                Swap(store, max, index);
                index = max;
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
    } 


    //
    public class MedianFinder2
    {
        private int counter = 0;
        private SortedSet<int[]> setLow = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) => a[0] == b[0] ? a[1] - b[1] : a[0] - b[0]));
        private SortedSet<int[]> setHigh = new SortedSet<int[]>(Comparer<int[]>.Create((a, b) => a[0] == b[0] ? a[1] - b[1] : a[0] - b[0]));
        public void AddNum(int num)
        {
            int[] newNum = new int[2] { num, counter++ };
            if (setLow.Count == setHigh.Count)
            {
                if (setLow.Count == 0 || newNum[0] <= setLow.Max[0]) setLow.Add(newNum);
                else
                {
                    setHigh.Add(newNum);
                    setLow.Add(setHigh.Min);
                    setHigh.Remove(setHigh.Min);
                }
            }
            else if (newNum[0] <= setLow.Max[0])
            {
                setLow.Add(newNum);
                setHigh.Add(setLow.Max);
                setLow.Remove(setLow.Max);
            }
            else setHigh.Add(newNum);
        }
        // return the median of current data stream
        public double FindMedian()
        {
            if (setLow.Count == 0) return 0;
            if (setLow.Count == setHigh.Count) return (setLow.Max[0] + setHigh.Min[0]) / 2d;
            else return setLow.Max[0];
        }
    }
}
