using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class GroupDigit
    {
        // Grouping Digits
        // similar as sort color problem
        // min swap to group
        public static int MinMoves(int[] input)
        {
            int n = input.Length;
            if (n <= 1)
                return 0;
            int[] oneOrZeroAtLeft = new int[2];

            for (int k = 0; k < 2; k++)
            {
                int first = 0;
                for (int i = 0; i < n; i++)
                {
                    if (input[i] == k)
                    {
                        oneOrZeroAtLeft[k] += Math.Abs(i - first);  // because we can only swap adjacent, so instead of adding 1,its i-first
                        first++;
                    }
                }
            }
            return Math.Min(oneOrZeroAtLeft[0], oneOrZeroAtLeft[1]);
        }
    }
}
