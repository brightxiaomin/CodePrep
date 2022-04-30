using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class BalancedString
    {
        // ways to split, balanced string, not fully working, but at least something.
        public static int SplitWays(string s)
        {
            int count = 0;

            Dictionary<char, int> leftBrackets = new Dictionary<char, int>();
            Dictionary<char, int> rightBrackets = CountBrackets(s);
            leftBrackets[s[0]] = 1;
            rightBrackets[s[0]]--;

            for (int i = 1; i < s.Length - 1; i++)
            {
                leftBrackets[s[i]] = leftBrackets.GetValueOrDefault(s[i], 0) + 1;
                rightBrackets[s[i]]--;

                if (IsBalanced(leftBrackets) && IsBalanced(rightBrackets))
                {
                    count++;
                }
            }

            return count;
        }

        private static bool IsBalanced(Dictionary<char, int> brackets)
        {
            int rdOpen = brackets.GetValueOrDefault('(', 0);
            int rdClose = brackets.GetValueOrDefault(')', 0);
            int sqOpen = brackets.GetValueOrDefault('[', 0);
            int sqClose = brackets.GetValueOrDefault(']', 0);
            int questionMark = brackets.GetValueOrDefault('?', 0);

            int rdDiff = Math.Abs(rdOpen - rdClose);
            int sqDiff = Math.Abs(sqOpen - sqClose);
            int diff = rdDiff + sqDiff;

            if (diff == 0 && questionMark % 2 == 0)
            {
                return true;
            }

            questionMark -= diff;

            if (questionMark < 0)
            {
                return false;
            }

            return questionMark % 2 == 0;
        }

        private static Dictionary<char, int> CountBrackets(string s)
        {
            Dictionary<char, int> charFreq = new Dictionary<char, int>();

            foreach (char c in s)
            {
                charFreq[c] = charFreq.GetValueOrDefault(c, 0) + 1;
            }

            return charFreq;
        }
    }
}
