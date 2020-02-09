using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class Palindrome
    {
        // can delete at most 1 character, decide whether we can make a palindrome
        // Valid Palindrome II
        public bool ValidPalindrome(string s)
        {
            int i = 0, j = s.Length - 1;
            while (i < j)
            {
                if (s[i] != s[j])
                {
                    if (s[i + 1] == s[j])
                    {
                        int a = i + 1, b = j;
                        while (a < b)
                        {
                            if (s[a] != s[b]) break;
                            a++; b--;
                        }

                        if (a >= b) return true;
                    }

                    if (s[i] == s[j - 1])
                    {
                        int a = i, b = j - 1;
                        while (a < b)
                        {
                            if (s[a] != s[b]) break;
                            a++; b--;
                        }
                        if (a >= b) return true;
                    }

                    return false;
                }
                i++; j--;
            }
            return true;

        }

        public bool IsValidPalindrome(string s)
        {
            int i = 0, j = s.Length - 1;
            while(i < j)
            {
                if(s[i] != s[j])
                {
                    return IsPalindrome(s, i + 1, j) || IsPalindrome(s, i, j - 1);
                }
                i++; j--;
            }
            return true;
        }

        //helper method to simplify the above code
        private bool IsPalindrome(string s, int i, int j)
        {
            while (i < j)
            {
                if (s[i] != s[j])
                    return false;
                i++; j--;
            }
            return true;
        }

        // 5. Longest Palindromic Substring
        // going to use DP, states dp[i,j] mean whether substring(i, j) is palindrome or not
        // idea is sub(i,j) = sub(i + 1, j - 1) && s[i] == s[j], be careful about  j -  i < 3, which means, j = i, i + 1, i +2, j is i, none in between, one in between, we dont need to check anything
        // starting from n - 1 position all the way up to 0,
        // each position, we count i up to n -1, so we only the fill in the top right half of the matrix
        public string LongestPalindrome(string s)
        {
            //string res = string.Empty;
            int n = s.Length, max = 0, start = 0;
            bool[,] dp = new bool[n, n];

            //starting dp, bottom up approach, starting from smallest problem (from end point len - 1), to biggest problem(starting point 0)
            for(int i  = n - 1; i >= 0; i--)
            {
                for(int j = i; j < n; j++)  
                {
                    if (s[j] == s[i] && (j - i < 3 || dp[i + 1, j - 1]))
                        dp[i, j] = true;

                    // when we found a palindrome, update max
                    if(dp[i,j] && j - i + 1 > max)
                    {
                        max = j - i + 1;
                        start = i;
                    }                    
                }
            }
            return s.Substring(start, max);
        }

        //optimize space a little to O(n)
        public string LongestPalindrome2(string s)
        {
            int n = s.Length, max = 0, start = 0;
            bool[] pre = new bool[n];
            bool[] cur = new bool[n];

            //two arrays to save
            for (int i = n - 1; i >= 0; i--)
            {
                bool lastLeft = pre[i];
                for (int j = i; j < n; j++)
                {
                    if (s[j] == s[i] && (j - i < 3 || lastLeft))
                        cur[j] = true;
                    else
                        cur[j] = false;  // important, previous 2-d array, it is okay, 

                    // when we found a palindrome, update max
                    if (cur[j] && j - i + 1 > max)
                    {
                        max = j - i + 1;
                        start = i;
                    }
                    lastLeft = pre[j];
                    pre[j] = cur[j];
                }
            }
            return s.Substring(start, max);
        }


        //one row
        public string LongestPalindrome3(string s)
        {
            int n = s.Length, max = 0, start = 0;

            bool[] cur = new bool[n];

            //one array to save
            for (int i = n - 1; i >= 0; i--)
            {
                bool lastLeft = cur[i];
                for (int j = i; j < n; j++)
                {
                    bool lastCurrent = cur[j];
                    if (s[j] == s[i] && (j - i < 3 || lastLeft))
                        cur[j] = true;
                    else
                        cur[j] = false;  // important, previous 2-d array, it is okay, 

                    // when we found a palindrome, update max
                    if (cur[j] && j - i + 1 > max)
                    {
                        max = j - i + 1;
                        start = i;
                    }
                    lastLeft = lastCurrent;
                    lastCurrent = cur[j];
                }
            }
            return s.Substring(start, max);
        }


    }


}
