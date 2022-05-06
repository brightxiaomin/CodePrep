using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class MinStepsToMakePileEqual
    {
        // working solution to me, O(nlogn)
        public static int MinSteps(int[] piles)
        {
            Array.Sort(piles);
            int sum = 0;
            int n = piles.Length;
            for (int i = 1; i < n; i++)
            {
                if (piles[n - i - 1] != piles[n - i])
                {
                    sum += i;
                }
            }
            return sum;
        }
    }
}
