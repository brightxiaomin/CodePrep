using System;
using System.Collections.Generic;

namespace MicrosoftOA
{
    class Program
    {
        static void Main(string[] args)
        {
            int test = Convert(423, 40);
            Console.WriteLine(test);
            Console.WriteLine("Hello World!");
        }

        static int Convert(int N, int k)
        {
            int remaining = k;
            int div = 100, res = 0;
            while(div != 0)
            {
                int temp = N / div;
                int digit = remaining > 0 ? 9 - temp <= remaining ? 9 : temp + remaining : temp;
                remaining -=  9 - temp;
                res += digit * div;
                N %= div;
                div /= 10;
            }

            return res;
        }

        public int CountServers(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;
            int numRows = grid.Length;
            int numCols = grid[0].Length;
            int[] rowCount = new int[numRows];
            int[] colCount = new int[numCols];
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (grid[row][col] == 1)
                    {
                        rowCount[row]++;
                        colCount[col]++;
                    }
                }
            }

            int result = 0;
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (grid[row][col] == 1 && (rowCount[row] > 1 || colCount[col] > 1))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public int MaxNonOverlapping(int[] nums, int target)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 0);

            int res = 0;
            int sum = 0;

            for (int i = 0; i < nums.Length; ++i)
            {
                sum += nums[i];
                if (map.ContainsKey(sum - target))
                {
                    res = Math.Max(res, map[sum - target] + 1);
                }
                if (!map.ContainsKey(sum))
                    map.Add(sum, res);
                else
                    map[sum] = res;
            }

            return res;
        }

        public int maxNonOverlapping(int[] nums, int target)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            int prefixSum = 0, availableIdx = -1, res = 0;
            map.Add(0, -1);
            for (int i = 0; i < nums.Length; i++)
            {
                prefixSum += nums[i];
                int remain = prefixSum - target;
                if (map.ContainsKey(remain) && map[remain] >= availableIdx)
                {
                    res++;
                    availableIdx = i;
                }

                if (!map.ContainsKey(prefixSum))
                    map.Add(prefixSum, res);
                else
                    map[prefixSum] = res;
            }
            return res;
        }
    }
}
