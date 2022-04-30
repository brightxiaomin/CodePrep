using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class SumSubrrayRange
    {
        // sum of subarray range
        public long SubArrayRanges(int[] A)
        {
            long res = 0;
            for (int i = 0; i < A.Length; i++)
            {
                int max = A[i], min = A[i];
                for (int j = i; j < A.Length; j++)
                {
                    max = Math.Max(max, A[j]);
                    min = Math.Min(min, A[j]);
                    res += max - min;
                }
            }
            return res;
        }

        public long SubArrayRangeUsingStack(int[] A)
        {
            int n = A.Length, j, k;
            long res = 0;

            Stack<int> s = new Stack<int>();
            for (int i = 0; i <= n; i++)
            {
                while (s.Count > 0 && A[s.Peek()] > (i == n ? int.MinValue : A[i]))
                {
                    j = s.Pop();
                    k = s.Count == 0 ? -1 : s.Peek();
                    res -= (long)A[j] * (i - j) * (j - k);

                }
                s.Push(i);
            }

            s.Clear();

            for (int i = 0; i <= n; i++)
            {
                while (s.Count > 0 && A[s.Peek()] < (i == n ? int.MaxValue : A[i]))
                {
                    j = s.Pop();
                    k = s.Count == 0 ? -1 : s.Peek();
                    res += (long)A[j] * (i - j) * (j - k);

                }
                s.Push(i);
            }
            return res;
        }
    }
}
