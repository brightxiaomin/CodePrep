using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    // Time O(N2), Space O(1)
    // https://www.geeksforgeeks.org/count-minimum-swap-to-make-string-palindrome/
    class MinSwapToMakeStringPalindrome
    {

        // Function to Count minimum swap
        static int countSwap(String str)
        {

            // Length of string
            int n = str.Length;

            // it will convert string to
            // char array
            char[] s = str.ToCharArray();

            // Counter to count minimum
            // swap
            int count = 0;

            // A loop which run in half
            // string from starting
            for (int i = 0; i < n / 2; i++)
            {

                // Left pointer
                int left = i;

                // Right pointer
                int right = n - left - 1;

                // A loop which run from
                // right pointer to left
                // pointer
                while (left < right)
                {

                    // if both char same
                    // then break the loop
                    // if not same then we
                    // have to move right
                    // pointer to one step
                    // left
                    if (s[left] == s[right])
                    {
                        break;
                    }
                    else
                    {
                        right--;
                    }
                }

                // it denotes both pointer at
                // same position and we don't
                // have sufficient char to make
                // palindrome string
                if (left == right)
                {
                    return -1;
                }
                else
                {
                    for (int j = right; j < n - left - 1; j++)
                    {
                        char t = s[j];
                        s[j] = s[j + 1];
                        s[j + 1] = t;
                        count++;
                    }
                }
            }

            return count;
        }

        // min swap to make 0/1 palindrome
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
