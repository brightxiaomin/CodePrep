using System;
using System.Collections.Generic;

namespace AmazonOA
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = DistinctLetterString("ABC");

            var tt = new Solution();
            int[][] test = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 } };

            int ss = tt.ShortestPathBinaryMatrix(test);

            Console.WriteLine("Hello World!");
        }


        // Grouping Digits
        // similar as sort color problem
        public static int MinMoves(int[] input)
        {
            int n = input.Length;
            if (n <= 1)
                return 0;
            int[] oneOrZeroAtLeft = new int[2];

            for (int k = 0; k < 2; k++)
            {
                int first = 0;
                for (int i = 0; i < n; i++)
                {
                    if (input[i] == k)
                    {
                        oneOrZeroAtLeft[k] += Math.Abs(i - first);  // because we can only swap adjacent, so instead of adding 1,its i-first
                        first++;
                    }
                }
            }
            return Math.Min(oneOrZeroAtLeft[0], oneOrZeroAtLeft[1]);
        }


        // max number of engineers
        int MaxTeams(int teamSize, int maxDiff, int[] skills)
        {
            if (teamSize == 1)
                return skills.Length;
            Array.Sort(skills);
            int teams = 0;
            int leastSkilled = 0;
            for (int i = 1; i < skills.Length; ++i)
            {
                while (skills[i] - skills[leastSkilled] > maxDiff)
                    ++leastSkilled;
                if (i - leastSkilled + 1 == teamSize)
                {
                    ++teams;
                    leastSkilled = i + 1;
                }
            }
            return teams;
        }

        // flip string
        public int MinFlipsMonoIncr(string s)
        {
            int N = s.Length;
            int[] P = new int[N + 1];
            for (int i = 0; i < N; ++i)
                P[i + 1] = P[i] + (s[i] == '1' ? 1 : 0);

            int ans = int.MaxValue;
            for (int j = 0; j <= N; ++j)
            {
                ans = Math.Min(ans, P[j] + N - j - (P[N] - P[j]));
            }

            return ans;
        }

        // sum of subarray range
        public long SubArrayRanges(int[] A)
        {
            long res = 0;
            for (int i = 0; i < A.Length; i++)
            {
                int max = A[i], min = A[i];
                for (int j = i; j < A.Length; j++)
                {
                    max = Math.Max(max, A[j]);
                    min = Math.Min(min, A[j]);
                    res += max - min;
                }
            }
            return res;
        }

        public long SubArrayRanges2(int[] A)
        {
            int n = A.Length, j, k;
            long res = 0;

            Stack<int> s = new Stack<int>();
            for (int i = 0; i <= n; i++)
            {
                while (s.Count > 0 && A[s.Peek()] > (i == n ? int.MinValue : A[i]))
                {
                    j = s.Pop();
                    k = s.Count == 0 ? -1 : s.Peek();
                    res -= (long)A[j] * (i - j) * (j - k);

                }
                s.Push(i);
            }

            s.Clear();

            for (int i = 0; i <= n; i++)
            {
                while (s.Count > 0 && A[s.Peek()] < (i == n ? int.MaxValue : A[i]))
                {
                    j = s.Pop();
                    k = s.Count ==0  ? -1 : s.Peek();
                    res += (long)A[j] * (i - j) * (j - k);

                }
                s.Push(i);
            }
            return res;
        }

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
        public int UniqueLetterStringII(String s)
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


        // ways to split, balanced string, not fully working, but at least something.
        private static int SplitWays(string s)
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
