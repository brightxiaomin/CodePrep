using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicrosoftOA
{
    // Equal Sum Arrays With Minimum Number of Operations
    // I suggest going through the heap solution below to get a good feel of this problem. 
    // In this solution, we use the same approach, but we count numbers [1...6] in each array and then calculate how many of each number we need to change.

    // In a way, we go from O(n log n) to O(n) by using counting sort.
    class MinOperationsEqualSum
    {
        public int MinOperations(int[] n1, int[] n2)
        {
            if (n2.Length * 6 < n1.Length || n1.Length * 6 < n2.Length)
                return -1;
            int sum1 = n1.Sum(), sum2 = n2.Sum();
            if (sum1 < sum2)
                return MinOperations(n1, n2, sum1, sum2);
            return MinOperations(n2, n1, sum2, sum1);
        }

        public int MinOperations(int[] n1, int[] n2, int sum1, int sum2)
        {
            int[] cnt = new int[6];
            int diff = sum2 - sum1, res = 0;
            foreach (var n in n1)
                ++cnt[6 - n];
            foreach (var n in n2)
                ++cnt[n - 1];
            for (int i = 5; i > 0 && diff > 0; --i)
            {
                int take = Math.Min(cnt[i], diff / i + (diff % i != 0 ? 1 : 0));
                diff -= take * i;
                res += take;
            }
            return res;
        }
    }
}
