using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class UniqueIntSumToZero
    {
        public int[] SumZero(int n)
        {
            List<int> res = new List<int>();
            for (int i = 1; i <= n / 2; i++)
            {
                res.Add(i);
                res.Add(-i);
            }
            if (n % 2 != 0)
                res.Add(0);

            return res.ToArray();
        }
    }
}
