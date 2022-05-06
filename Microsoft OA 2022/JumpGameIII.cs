using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class JumpGameIII
    {
        // jump game
        public bool CanReach(int[] arr, int start)
        {
            int n = arr.Length;

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);

            while (q.Count > 0)
            {
                int node = q.Dequeue();
                // check if reach zero
                if (arr[node] == 0)
                {
                    return true;
                }
                if (arr[node] < 0)
                {
                    continue;
                }

                // check available next steps
                if (node + arr[node] < n)
                {
                    q.Enqueue(node + arr[node]);
                }
                if (node - arr[node] >= 0)
                {
                    q.Enqueue(node - arr[node]);
                }
                // mark as visited
                arr[node] = -arr[node];
            }
            return false;
        }
    }
}
