using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class MinRoofToCoverCars
    {
        // min roof
        public static int MinRoof(int[] pos, int k)
        {
            Array.Sort(pos);
            int min = int.MaxValue;
            if (pos.Length == 0 || pos.Length < k)
                return 0;
            for (int i = 0; i + k - 1 < pos.Length; i++)
            {

                min = Math.Min(min, pos[i + k - 1] - pos[i] + 1);

            }
            return min;
        }
    }
}
