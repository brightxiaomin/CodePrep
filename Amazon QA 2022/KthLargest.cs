using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    // k largest in a data stream
    public class KthLargest
    {

        private int k;
        private SimpleHeap heap = new SimpleHeap();

        public KthLargest(int k, int[] nums)
        {
            this.k = k;
            foreach (int num in nums)
            {
                heap.Add(num);
                if (heap.GetSize() > k)
                    heap.Pop();
            }
        }

        public int Add(int val)
        {
            heap.Add(val);
            if (heap.GetSize() > k)
                heap.Pop();

            return heap.Peek();
        }
    }

    public class SimpleHeap
    {
        private static readonly int DEFAULT_CAPACITY = 11;
        private int used;
        private int[] store;

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
}
