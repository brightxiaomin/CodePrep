using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class PasswordStrength
    {

        // LC828 password strength, if unique use this one
        public int UniqueLetterString(string S)
        {

            int res = 0;
            if (S == null || S.Length == 0)
                return res;
            int[] showLastPosition = new int[128];
            int[] contribution = new int[128];
            int cur = 0;
            for (int i = 0; i < S.Length; i++)
            {
                char x = S[i];
                cur -= contribution[x]; // remove previous contribution
                contribution[x] = (i + 1 - showLastPosition[x]);
                cur += contribution[x];  // add current contribution
                showLastPosition[x] = i + 1;
                res += cur;
            }
            return res;
        }

        // pass word strength, if distinct, this way
        public static int DistinctLetterString(string s)
        {

            int res = 0;
            if (s == null || s.Length == 0)
                return res;
            int[] lastIndexArray = new int[26];
            for (int i = 0; i < 26; i++)
                lastIndexArray[i] = -1;
            //int[] contribution = new int[128];
            int cur = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char x = s[i];
                int lastIndex = lastIndexArray[x - 'A'];
                cur = cur + i + 1 - (lastIndex + 1);
                res += cur;
                //update last index
                lastIndexArray[x - 'A'] = i;
            }
            return res;
        }


        // idea is to process letter by letter
        public int UniqueLetterStringII(string s)
        {
            int[] lastPosition = new int[26];
            int[] contribution = new int[26];
            int res = 0;

            //     Basically, at each it, we count the contribution of all the characters to all the substrings ending till that point.

            for (int i = 0; i < s.Length; i++)
            {

                int curChar = s[i] - 'A';

                //       Now, we need to update the contribution of curChar.
                //       The total number of substrings ending at i are i+1. So if it was a unique character, it'd contribute to all of those
                //       and it's contribution would have been i+1.
                //       But if it's repeating, it means it has already contributed previously. So remove it's previous contribution.
                //       We can do that as we have it's last position.
                //       So these are the contributions for strings which start after this character's last occurrence and end at i.
                //       A simple example will demonstrate that the number of these strings are i+1 - lastPosition[curChar]
                //       For characters not appeared till now, lastPosition[curChar] would be 0.
                int totalNumOfSubstringsEndingHere = i + 1;
                contribution[curChar] = totalNumOfSubstringsEndingHere - lastPosition[curChar];

                //       Note that, the contribution of all the other characters will remain same.

                //       count the cur answer by summing all the contributions. This loop can be avoided by the idea in original post, but I find
                //       it easy to understand with this and it only iterates over 26 values.
                int cur = 0;
                for (int j = 0; j < 26; j++)
                {
                    cur += contribution[j];
                }

                //       add the current value to final answer.
                res += cur;

                //       update the last position of this char. This helps in future to count it's contribution if it appears again.
                lastPosition[curChar] = i + 1;
            }
            return res;
        }
    }
}
