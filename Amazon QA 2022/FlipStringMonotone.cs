using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class FlipStringMonotone
    {
        // flip string
        public int MinFlipsMonoIncr(string s)
        {
            int N = s.Length;
            int[] P = new int[N + 1];
            for (int i = 0; i < N; ++i)
                P[i + 1] = P[i] + (s[i] == '1' ? 1 : 0);

            int ans = int.MaxValue;
            for (int j = 0; j <= N; ++j)
            {
                ans = Math.Min(ans, P[j] + N - j - (P[N] - P[j]));
            }

            return ans;
        }
    }
}
