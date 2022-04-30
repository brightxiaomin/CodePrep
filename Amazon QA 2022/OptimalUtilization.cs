using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class OptimalUtilization
    {
        // Optimal Utilization
        public List<int[]> GetPairs(List<int[]> a, List<int[]> b, int target)
        {
            a.Sort((i, j) => i[1] - j[1]);
            b.Sort((i, j) => i[1] - j[1]);
            List<int[]> result = new List<int[]>();
            int max = int.MinValue;
            int m = a.Count;
            int n = b.Count;
            int i = 0;
            int j = n - 1;
            while (i < m && j >= 0)
            {
                int sum = a[i][1] + b[j][1];
                if (sum > target)
                {
                    --j;
                }
                else
                {
                    if (max <= sum)
                    {
                        if (max < sum)
                        {
                            // we got even closer pair 
                            max = sum;
                            result.Clear();
                        }
                        result.Add(new int[] { a[i][0], b[j][0] });
                        int index = j - 1;
                        while (index >= 0 && b[index][1] == b[index + 1][1])
                        {
                            result.Add(new int[] { a[i][0], b[index][0] });
                            index--;
                        }
                    }
                    ++i;
                }
            }
            return result;
        }
    }
}
