using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    public class MaxDev
    {
        int MaxSubArraySum(List<int> arr, int k)
        {
            if (arr.Count < k)
                return 0;

            int n = arr.Count;
            int[] maxSum = new int[n];

            // use kadane's
            maxSum[0] = arr[0];
            for (int i = 1; i < arr.Count; i++)
            {
                maxSum[i] = Math.Max(arr[i], maxSum[i - 1] + arr[i]);
            }

            int sum = 0;
            for (int i = 0; i < k; i++)
            {
                sum += arr[i];
            }

            int ans = sum;
            for (int i = k; i < arr.Count; i++)
            {
                sum = sum + arr[i] - arr[i - k];
                ans = Math.Max(ans, sum);
                ans = Math.Max(ans, sum + maxSum[i - k]);
            }

            return ans;
        }

        int MaxDeviation(string str)
        {
            int ans = 0;

            for (char c1 = 'a'; c1 <= 'z'; c1++)
            {
                for (char c2 = 'a'; c2 <= 'z'; c2++)
                {
                    if (c1 == c2)
                        continue;

                    List<int> arr = new List<int>();
                    // we consider c1 as character with maxFreq and c2 with minFreq
                    foreach (char c in str)
                    {
                        if (c == c1)
                        {
                            // We shall include all consecutive c1's in our array so we add their frequency
                            if (arr.Count > 0 && arr[arr.Count - 1] != -1)
                            {
                                arr[arr.Count - 1] += 1;
                            }
                            else
                            {
                                arr.Add(1);
                            }
                        }
                        else if (c == c2)
                        {
                            // we take distinct c2
                            arr.Add(-1);
                        }
                    }
                    ans = Math.Max(ans, MaxSubArraySum(arr, 2));
                }
            }
            return ans;
        }
    }
  
}
