using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    class WordSearch
    {
        //I have some idea about it, using DFS, I know how to solve with one start node
        //but this one have all possible starting node, so the actually search, is to try every possible
        // starting node, I am trying to optimize at very beginning, no need. just get the answer first.
        //DFS mix with backtrack (normal DFS, terminating is reaching boundary, while backtrack, it is not found a solution, not valid step)
        public bool Exist(char[][] board, string word)
        {
            //start from all nodes
            for(int i = 0; i < board.Length; i++)
            {
                for(int j  =0; j < board[0].Length; j++)
                {
                    // anytime it is found, method return.
                    if (DFS(board, word, i, j, 0)) return true;
                }
            }

            return false;
        }

        // there is a bug, cannot reuse letter inside one try
        private bool DFS(char[][] board, string word, int row, int col, int pos)
        {
            // found
            if (pos == word.Length) return true;
            // out of grid
            if (row < 0 || row >= board.Length || col < 0 || col >= board[0].Length)
                return false;

            char temp = board[row][col];
            if (temp != word[pos]) return false;

            // make sure same node is not reused
            board[row][col] = '*';

            // run four directions, if first true, second not exectued. 
           bool exist=  DFS(board, word, row + 1, col, pos + 1) || DFS(board, word, row - 1, col, pos + 1)
                 || DFS(board, word, row, col + 1, pos + 1) || DFS(board, word, row, col - 1, pos + 1);

            //this the point of backtrack, you go back, and another route, this node can be reused now
            board[row][col] = temp;

            return exist;
        }


        //word search II, kind of brutal force
        public IList<string> FindWords(char[][] board, string[] words)
        {
            List<string> ret = new List<string>();
            foreach (string word in words)
            {
                if (Exist(board, word))
                    ret.Add(word);
            }
            return ret;
        }
    }


    public class Solution
    {
        public IList<int> TopKFrequent(int[] nums, int k)
        {
            List<int> output = new List<int>();
            Dictionary<int, int> fre = new Dictionary<int, int>();

            foreach (int s in nums)
            {
                if (!fre.ContainsKey(s))
                {
                    fre.Add(s, 1);
                    continue;
                }
                fre[s] = fre[s] + 1;
            }
            nums = fre.Keys.ToArray(); // remove duplicates in dict
            int[] heap = new int[k];

            //insert the k words in heap
            for (int i = 0; i < k; i++)
            {
                heap[i] = nums[i];
                ReCalculateUp(heap, i, fre);
            }

            for (int i = k; i < nums.Length; i++)
            {
                if (Compare(nums[i], heap[0], fre) > 0)
                {
                    heap[0] = nums[i];
                    ReCalculateDown(heap, 0, k, fre);
                }
            }

            for (int i = k - 1; i >= 0; i--)
            {
                output.Add(heap[0]);
                heap[0] = heap[i];
                ReCalculateDown(heap, 0, i, fre);
            }

            output.Reverse();

            return output;
        }

        public void ReCalculateDown(int[] arr, int index, int size, Dictionary<int, int> freq)
        {
            while (true)
            {
                int min = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                if (left < size && Compare(arr[min], arr[left], freq) > 0) min = left;
                if (right < size && Compare(arr[min], arr[right], freq) > 0) min = right;
                if (min == index) return; // NO swap

                Swap(arr, min, index);
                index = min;
            }
        }

        public void ReCalculateUp(int[] arr, int index, Dictionary<int, int> freq)
        {
            // has parent node and parent value > child value
            while (index > 0 && Compare(arr[(index - 1) / 2], arr[index], freq) > 0)
            {
                Swap(arr, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public int Compare(int k1, int k2, Dictionary<int, int> freq)
        {
            if (freq[k1] != freq[k2])
                return freq[k1] - freq[k2];

            return k2.CompareTo(k1);
        }

        public void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
