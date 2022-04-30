using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class MinSwapToMakePalindrome
    {
        // min swap to make palindrome
        public static int MinSwapsPal(string str)
        {

            int countOne = 0;
            int countZero = 0;

            foreach (char c in str)
            {
                if (c == '0')
                {
                    countZero++;
                }
                else
                {
                    countOne++;
                }
            }

            if (countOne % 2 == 1 && countZero % 2 == 1)
            {
                return -1;
            }

            int mismatchedIndexes = 0;
            for (int i = 0; i < (str.Length / 2); i++)
            {
                if (str[i] != str[str.Length - 1 - i])
                {
                    mismatchedIndexes++;
                }
            }

            int countSwaps = mismatchedIndexes / 2;
            if (mismatchedIndexes % 2 == 1)
            {
                countSwaps++;
            }

            return countSwaps;
        }
    }
}
