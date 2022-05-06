using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class MinMeetingRooms
    {
        public int MinMeetingRoomCount(int[][] intervals)
        {
            int used = 0;
            int len = intervals.Length;
            //two arrays
            int[] start = new int[len];
            int[] end = new int[len];

            for (int i = 0; i < len; i++)
            {
                start[i] = intervals[i][0];
                end[i] = intervals[i][1];
            }

            //sort two array, in time order]
            Array.Sort(start);
            Array.Sort(end);

            int str = 0, etr = 0;
            //process two pointers
            while (str < len)
            {
                //has vacant room
                if (start[str] >= end[etr])
                {
                    used--;  //decrement anyway, because I am going to increment
                             // move etr to new room
                    etr++;
                }

                used++;
                str++;
            }
            return used;
        }

        int minMeetingRooms(int[][] meetings)
        {
            int n = meetings.Length;
            int[] begin = new int[n];
            int[] end = new int[n];
            for (int k = 0; k < n; k++)
            {
                begin[k] = meetings[k][0];
                end[k] = meetings[k][1];
            }
            Array.Sort(begin);
            Array.Sort(end);

            // 扫描过程中的计数器
            int count = 0;
            // 双指针技巧
            int res = 0, i = 0, j = 0;
            while (i < n && j < n)
            {
                if (begin[i] < end[j])
                {
                    // 扫描到一个红点
                    count++;
                    i++;
                }
                else
                {
                    // 扫描到一个绿点
                    count--;
                    j++;
                }
                // 记录扫描过程中的最大值
                res = Math.Max(res, count);
            }

            return res;
        }
    }
}
