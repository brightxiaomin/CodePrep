using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class ObjectHeap
    {
        private static readonly int DEFAULT_CAPACITY = 11;
        private int used;
        private Cell[] store;

        public ObjectHeap() : this(DEFAULT_CAPACITY)
        {

        }

        public ObjectHeap(int cap)
        {
            used = 0;
            store = new Cell[cap];
        }

        public bool IsEmpty()
        {
            return used == 0;
        }

        public int GetSize()
        {
            return used;
        }

        public Cell Peek()
        {
            if (used == 0)
                throw new IndexOutOfRangeException();

            return store[0];
        }

        public Cell Pop()
        {
            if (used == 0)
                throw new IndexOutOfRangeException();

            var result = store[0];
            store[0] = store[used - 1];
            used--;

            Sink();
            return result;
        }

        public void Add(Cell val)
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

        public bool Compare(Cell a, Cell b)
        {
            // what ever compare we want, here we want a max hepa.
            return a.val > b.val;

            // if min heap, reversed
            // return a.val < b.val
        }

        public void Swap(Cell[] arr, int a, int b)
        {
            Cell temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        void Resize()
        {
            Cell[] temp = new Cell[2 * store.Length];
            store.CopyTo(temp, 0);
            store = temp;
        }
    }

    public struct Cell
    {
        public int row;
        public int col;
        public int val;
        public Cell(int row, int col, int val)
        {
            this.row = row;
            this.col = col;
            this.val = val;
        }
    }

    public class Solution2
    {

        public int MaximumMinimumPath(int[][] A)
        {
            int n = A.Length;
            int m = A[0].Length;
            bool[][] visited = new bool[n][];
            for (int i = 0; i < n; i++)
                visited[i] = new bool[m];

            // in the BFS approach, for each step, we are interested in getting the maximum min that we have seen so far, thus we reverse the ordering in the pq
            ObjectHeap heap = new ObjectHeap(1000);

            heap.Add(new Cell(0, 0, A[0][0]));
            visited[0][0] = true;
            int[] dirR = { 0, 0, 1, -1 };
            int[] dirC = { 1, -1, 0, 0 };
            // BFS
            while (!heap.IsEmpty())
            {
                Cell cell = heap.Pop();
                int row = cell.row;
                int col = cell.col;

                if (row == n - 1 && col == m - 1)
                {
                    return cell.val;
                }



                for (int i = 0; i < 4; i++)
                {
                    int nextRow = row + dirR[i];
                    int nextCol = col + dirC[i];

                    if (nextRow < 0 || nextRow >= n || nextCol < 0 || nextCol >= m || visited[nextRow][nextCol]) continue;

                    // we are keeping track of the min element that we have seen until now
                    heap.Add(new Cell(nextRow, nextCol, Math.Min(cell.val, A[nextRow][nextCol])));
                    visited[nextRow][nextCol] = true;
                }
            }
            return -1;
        }
    }
}
