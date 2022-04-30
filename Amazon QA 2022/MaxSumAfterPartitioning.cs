using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class MaxSumAfterPartitioning
    {
        // Partition Array for Maximum Sum
        // Think dynamic programming: dp[i] will be the answer for array A[0], ..., A[i].
        // For j = 1 .. k that keeps everything in bounds, dp[i] is the maximum of dp[i-j] + max(A[i], ..., A[i-j+1]) * j .
        public int MaxSumAfterPartitioning2(int[] A, int K)
        {
            int N = A.Length;
            int[] dp = new int[N];
            for (int i = 0; i < N; ++i)
            {
                int curMax = 0;
                for (int j = 1; j <= K && i - j + 1 >= 0; ++j)
                {
                    curMax = Math.Max(curMax, A[i - j + 1]);
                    dp[i] = Math.Max(dp[i], (i >= j ? dp[i - j] : 0) + curMax * j);
                }
            }
            return dp[N - 1];
        }
    }
}
