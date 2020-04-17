using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class PHeap  // dynamic heap
    {
        public int used;
        public int capacity;
        public int[] store;

        public PHeap(int cap)
        {
            used = 0;
            capacity = cap;
            store = new int[capacity];
        }

        public int Pop()
        {
            if (used == 0) throw new Exception("no element");
            int res = store[0];
            store[0] = store[used - 1];
            Sink();
            return res;
        }

        public int Peek()
        {
            if (used == 0) throw new Exception("no element");
            return store[0];
        }

        public void Add(int val)
        {
            if (used == capacity) Resize();
            store[used] = val;
            used++;
            BubbleUp();
        }

        public void BubbleUp()
        {
            int index = used - 1;
            while (index > 0 && Compare(store[index], store[(index - 1) / 2]))
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public void Sink()
        {
            int index = 0;
            while (true)
            {
                int current = index;
                int left = 2 * current + 1;
                int right = 2 * current + 2;
                if (left < used && Compare(store[current], store[left])) current = left;
                if (right < used && Compare(store[current], store[right])) current = right;
                //order is right, return
                if (current == index) return;
                Swap(index, current);
                index = current;
            }
        }

        public bool Compare(int child, int parent)
        {
            // min heap, child smaller, need to swap
            return store[child] < store[parent];
            // max heap, reversed
        }

        public void Swap(int a, int b)
        {
            int temp = store[a];
            store[a] = store[b];
            store[b] = temp;
        }

        public void Resize()
        {
            int[] temp = new int[2 * capacity];
            store.CopyTo(temp, 0);
            store = temp;
        }

    }
}
