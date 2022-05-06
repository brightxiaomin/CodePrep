using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class MaxSubstringLengthWithout3Consecutive
    {
        // Function to return the length of the
        // longest substring such that no three
        // consecutive characters are same
        static int maxLenSubStr(String s)
        {
            // If the length of the given string
            // is less than 3
            if (s.Length < 3)
                return s.Length;

            // Initialize temporary and final ans
            // to 2 as this is the minimum length
            // of substring when length of the given
            // string is greater than 2
            int temp = 2;
            int ans = 2;

            // Traverse the string from the
            // third character to the last
            for (int i = 2; i < s.Length; i++)
            {

                // If no three consecutive characters
                // are same then increment temporary count
                if (s[i] != s[i - 1] ||
                    s[i] != s[i - 2])
                    temp++;

                // Else update the final ans and
                // reset the temporary count
                else
                {
                    ans = Math.Max(temp, ans);
                    temp = 2;
                }
            }
            ans = Math.Max(temp, ans);

            return ans;
        }
    }
}
