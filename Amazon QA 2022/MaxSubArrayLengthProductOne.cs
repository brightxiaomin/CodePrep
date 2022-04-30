using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class MaxSubArrayLengthProductOne
    {
        // Given an array of only 1 and -1, find a subarray of maximum length such that the product of all the elements in the subarray is 1
        // iterate the array, store position of all -1,if count is even number,return whole array, else compare head to last -1, and tail to first 1
        static int max(int[] a)
        {
            int c = 0;
            int fnegindex = -1;//for the first negative index
            int lnegindex = -1;//for the last negative index
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == -1)
                {
                    if (fnegindex == -1)
                    {
                        fnegindex = i;
                    }
                    lnegindex = i;
                    c++;
                }
            }
            if (c % 2 == 0) return a.Length;
            else
            {
                return Math.Max(a.Length - fnegindex - 1, lnegindex);
            }
        }
    }
}
