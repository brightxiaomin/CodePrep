using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class CanJump
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
        public bool canJump2(int[] nums)
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
    }
}
