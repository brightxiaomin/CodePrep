using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class TwoSumAll
    {
        //O(N)
        public int UniquePairs(int[] nums, int target)
        {
            int count = 0;
            HashSet<int> used = new HashSet<int>();
            HashSet<int> set = new HashSet<int>();

            //because no index is need here, can use foreach
            // if need to return index, use for loop
            foreach(int num in nums)
            {
                if(set.Contains(target - num) && !used.Contains(num))
                {
                    count++;
                    used.Add(num);
                    used.Add(target - num);
                }
                else if(!set.Contains(num))
                {
                    set.Add(num);
                }
            }

            return count;
        }


        //find two numbers or indexes from two arrays, closet to target and sum<= target
        //O(nlogn) + O(mlogm) + O(M + N)
        //if m == n, then O(nlogn);
        public int[] TwoSumClosest(int[] arr1, int[] arr2, int target)
        {
            Array.Sort(arr1);
            Array.Sort(arr2);
            int[] result = new int[2];
            int minDiff = int.MaxValue;

            int lo = 0, hi = arr2.Length - 1;
            while(lo < arr1.Length && hi >= 0)
            {
                int diff = target - (arr1[lo] + arr2[hi]);
                if (diff > 0)
                {
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        result[0] = arr1[lo]; // if we want index, assign to lo
                        result[1] = arr2[hi];
                    }
                    lo++;
                }
                else if (diff < 0)
                {
                    hi--;
                }
                else
                {
                    result[0] = arr1[lo];
                    result[1] = arr2[hi];
                    break;
                }
            }

            return result;
        }

        //brutal force
        public List<int> solution(int target, List<int> foregroundSizes, List<int> backgroundSizes)
        { // O(n^2)
            List<int> res = new List<int>
            {
                0,
                0
            };
            int max = 0;
            for (int i = 0; i < foregroundSizes.Count; i++)
            {
                for (int j = 0; j < backgroundSizes.Count; j++)
                {
                    int cur = foregroundSizes[i] + backgroundSizes[j];
                    if (cur <= target && cur > max)
                    {
                        max = cur;
                        res[0] = i;
                        res[0] = j;
                    }
                }
            }
            return max == 0 ? new List<int>() : res;
        }

        //find two nums for two sum closest that sum <= target
        //kind of two pointers, O(n) runtime for the loop, O(nlogn) for the sort
        // so runtime is O(nlogn), space is O(1)
        public int[] TwoSumClosest(int[] nums, int target)
        {
            Array.Sort(nums);
            int[] result = new int[2];
            int minDiff = int.MaxValue;
            for (int lo = 0, hi = nums.Length - 1; lo < hi;)
            {
                int diff = target - (nums[lo] + nums[hi]);
                if (diff > 0)
                {
                    //update diff and result
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        result[0] = nums[lo]; // if we want index, assign to lo
                        result[1] = nums[hi];
                    }
                    lo++;
                }
                else if (diff < 0)
                {
                    hi--;
                }
                else
                {
                    result[0] = nums[lo];
                    result[1] = nums[hi];
                    break;
                }
            }
            return result;
        }


        //two sum cloest, return diff (absolute)
        public int TwoSumClosetDiff(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            Array.Sort(nums);
            int low = 0, high = nums.Length - 1;
            int diff = int.MaxValue;

            while (low < high)
            {
                int sum = nums[low] + nums[high];

                if (sum > target)
                {
                    high--;
                }
                else
                {
                    low++;
                }

                diff = Math.Min(diff, Math.Abs(sum - target));
            }

            return diff;
        }



        //find indexes for two sum equal target
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int x = nums[i];
                if (dict.ContainsKey(target - x))
                {
                    return new int[2] { i, dict[target - x] };
                }
                if (!dict.ContainsKey(x))
                    dict.Add(x, i);
            }
            throw new Exception("no two sum solution");
        }



    }
}
