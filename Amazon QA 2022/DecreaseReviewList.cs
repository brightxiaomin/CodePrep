using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class DecreaseReviewList
    {
        // decreasing review list, decreasing sublist
        // monotonic decrease
        public int DecreasingSubarray(int[] array)
        {
            Stack<int> stack = new Stack<int>();
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (stack.Count > 0 && stack.Peek() <= array[i])
                {
                    stack.Clear();
                }
                stack.Push(array[i]);
                count += stack.Count;
            }
            return count;
        }
    }
}
