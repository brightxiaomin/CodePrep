using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class ConsecutiveElementsByMinus1
    {

        // Function to count Subarrays with
        // Consecutive elements differing by 1
        public static int SubarrayCount(int[] arr, int n)
        {
            // Variable to store count of subarrays
            // whose consecutive elements differ by 1
            int result = 0;

            // Take two pointers for maintaining a
            // window of consecutive elements
            int fast = 0, slow = 0;

            // Traverse the array
            for (int i = 1; i < n; i++)
            {

                // If elements differ by 1
                // increment only the fast pointer
                if (arr[i] - arr[i - 1] == 1)
                {
                    fast++;
                }
                else
                {

                    // Calculate length of subarray
                    int len = fast - slow + 1;

                    // Calculate total subarrays except
                    // Subarrays with single element
                    result += len * (len - 1) / 2;

                    // Update fast and slow
                    fast = i;
                    slow = i;
                }
            }

            // For last iteration. That is if array is
            // traversed and fast > slow
            if (fast != slow)
            {
                int len = fast - slow + 1;
                result += len * (len - 1) / 2;
            }

            return result;
        }
    }
}
