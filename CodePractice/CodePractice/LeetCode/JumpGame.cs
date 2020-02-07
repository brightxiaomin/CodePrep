using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class JumpGame
    {
        public bool CanJumpM(int[] nums) 
        {
            return Jump(nums, nums.Length - 1);
        }

        public bool Jump(int[] nums, int i)
        {
            if (i == 0) return true;

            for (int j = i - 1; j >= 0; j--)
            {
                if (i - j <= nums[j] && Jump(nums, j))
                    return true;
            }

            return false;
        }

        //greedy 
        public bool CanJump2(int[] nums)
        {
            int lastPos = nums.Length - 1;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (i + nums[i] >= lastPos)
                {
                    lastPos = i;
                }
            }
            return lastPos == 0;
        }


        //min steps to reach last index
        //first DP, O(N*N)

        //TLE
        public int Jump(int[] nums)
        {
            int len = nums.Length;
            int[] min = Enumerable.Repeat(-1, len).ToArray();

            min[len - 1] = 0;
            for (int i = len - 2; i >= 0; i--)
            {
                if (nums[i] == 0) continue;
                //cannot reach last within one step
                if (i + nums[i] < len - 1)
                {
                    int tempMin = int.MaxValue;
                    //find min from i + 1 to i + nums[i]
                    //consider num[i] = 0
                    for (int j = 1; j <= nums[i]; j++)
                    {
                        if (min[i + j] == -1) continue;
                        if (min[i + j] < tempMin) tempMin = min[i + j];
                    }
                    min[i] = tempMin == int.MaxValue ? -1: tempMin + 1;  // tempMin still max means the values cannt reach last
                }
                else
                    min[i] = 1;
            }

            return min[0];
        }

        //Greedy
        //worest is O(n2), one step a time
        //two loops
        //currentLeftMost, current left most which can reach last level

        // was constrained by the greedy solution for Original jump game, going backward.
        public int JumpGreedy(int[] nums)
        {
            int step = 0, len = nums.Length, currentLeftMost = len - 1;

            while (currentLeftMost > 0)
            {
                step++;
                int temp = currentLeftMost;
                for (int i = temp - 1; i >= 0; i--)
                {
                    if (i + nums[i] >= currentLeftMost)
                        temp = i;
                }
                currentLeftMost = temp;
            }

            return step;
        }


        //jump forward
        public int JumpGreedy2(int[] nums)
        {
            int jumps = 0, curEnd = 0, curFarthest = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                curFarthest = Math.Max(curFarthest, i + nums[i]);
                if (i == curEnd)
                {
                    jumps++;
                    curEnd = curFarthest;

                    if (curEnd >= nums.Length - 1)
                    {
                        break;
                    }
                }
            }
            return jumps;
        }
    }
}
