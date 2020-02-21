using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class MedianFinder
    {
        /** initialize your data structure here. */
        public MedianFinder()
        {

        }

        //because no duplicate allow in the sorted set.
        private SortedSet<int> setLow = new SortedSet<int>();

        private SortedSet<int> setHigh = new SortedSet<int>();

        public void AddNum(int num)
        {
            bool twoTreesSameSize = setLow.Count == setHigh.Count;

            //keep setLow size >= setHigh
            if (twoTreesSameSize)
            {
                if (setLow.Count == 0 || num <= setLow.Max)
                {
                    setLow.Add(num);
                }
                else
                {
                    setHigh.Add(num);

                    // move the minimum number from setHigh to setLow. 
                    setLow.Add(setHigh.Min);
                    setHigh.Remove(setHigh.Min);
                }
            }
            else if (num <= setLow.Max)
            {
                setLow.Add(num);

                // move the maximum number from setLow to setHigh
                setHigh.Add(setLow.Max);
                setLow.Remove(setLow.Max);
            }
            else
            {
                setHigh.Add(num);
            }
        }

        public double FindMedian()
        {
            if (setLow.Count == 0)
            {
                return 0;
            }

            if (setLow.Count == setHigh.Count)
            {
                return (setLow.Max + setHigh.Min) / 2d;
            }
            else
            {
                return setLow.Max;
            }
        }
    }
}
