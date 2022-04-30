using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class WifiRange
    {
        // wifi router problem
        // use diff array
        public void increment(int i, int j, int val)
        {
            int[] diff = new int[10];
            diff[i] += val;
            if (j + 1 < diff.Length)
            {
                diff[j + 1] -= val;
            }
        }

        public int[] result()
        {

            int[] diff = new int[10];
            int[] res = new int[diff.Length];
            // 根据差分数组构造结果数组
            res[0] = diff[0];
            for (int i = 1; i < diff.Length; i++)
            {
                res[i] = res[i - 1] + diff[i];
            }
            return res;
        }

    }
}
