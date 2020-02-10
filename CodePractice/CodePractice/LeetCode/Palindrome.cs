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


        //516. Longest Palindromic Subsequence
        // DP to solve this
        // sort of like the longest common subsequence problem
        // use a 2-d array to store the max at i,j, here dp[i,j] means the longest (int) palindromic subsequence between substring (i,j)
        //dp[i,i] = 1; dp[i,j] = dp[i + 1, j -1] +  2 if (s[i] == s[j])
        //dp[i,j] = Max(dp[i + 1, j], dp[i, j -1])if (s[i] != s[j])
        // if we want to retrieve, we must have another 2-d array to store the selection like LCS problem
        // or we can do compare at each i, j to find out the selection.
        public int LongestPalindromeSubseq(string s)
        {
            int n = s.Length;
            int[,] dp = new int[n, n];

            //fill in dp 
            for(int i = n - 1; i >= 0; i--)
            {
                dp[i, i] = 1;
                for(int j = i + 1; j < n; j++)
                {
                    if (s[i] == s[j])
                        dp[i, j] = dp[i + 1, j - 1] + 2;
                    else
                    {
                        dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j - 1]);
                    }
                }
            }

            return dp[0, n - 1];
        }


        public int LongestPalindromeSubseq2(string s)
        {
            int n = s.Length;
            int[] cur = new int[n];
            int[] pre = new int[n];

            //fill in dp, after update current[i], need to also update pre[i] to cur[i] last
            for (int i = n - 1; i >= 0; i--)
            {
                cur[i] = 1;
                int lastLeft = pre[i];
                for (int j = i + 1; j < n; j++)
                {
                    if (s[i] == s[j])
                        cur[j] = lastLeft + 2;
                    else
                    {
                        cur[j] = Math.Max(pre[j], cur[j - 1]);
                    }
                    lastLeft = pre[j];
                    pre[j] = cur[j];
                }
                pre[i] = cur[i];
            }

            return cur[n - 1];
        }
        //optimize some space
        public int LongestPalindromeSubseq3(string s)
        {
            int n = s.Length;
            int[] dp = new int[n];
             
            //fill in dp, use it before its updated
            for (int i = n - 1; i >= 0; i--)
            {
                int lastLeft = dp[i];
                dp[i] = 1;
                for (int j = i + 1; j < n; j++)
                {
                    int lastCurrent = dp[j];
                    if (s[i] == s[j])
                        dp[j] = lastLeft + 2;
                    else
                    {
                        dp[j] = Math.Max(lastCurrent, dp[j - 1]);
                    }
                    lastLeft = lastCurrent;
                    lastCurrent = dp[j];
                }
            }

            return dp[n - 1];
        }

        //866. Prime Palindrome
        public int PrimePalindrome(int N)
        {
            //BF produce TLE
            int i = N;
            while(i < 200000000 )
            {
                if (IsPrimeNumber(i) && IsPalindromeNumber(i))
                    return i;
                i++;
            }
            return -1;
        }

        bool IsPrimeNumber(int num)
        {
            if (num == 0 || num == 1) return false;
            int R = (int)Math.Sqrt(num);
            for(int i = 2; i <= R; i++)
            {
                if (num % i == 0) return false;
            }
            return true;
        }

        bool IsPalindromeNumber(int num)
        {
            int div = 1, temp = num;
            while(temp / 10 != 0)
            {
                div *= 10;
                temp /= 10;
            }

            while(num != 0)
            {
                int left = num / div;
                int right = num % 10;
                if (left != right) return false;
                //chop two numbers
                num = (num % div) / 10;
                div /= 100;
            }

            return true;
        }

        //construct palindromic number in order
        //we skip even length palindrome numbers
        //according to number theory, 
        /*
         * O(10000) to check all numbers 1 - 100000.
        isPrime function is O(sqrt(x)) in worst case.
        But only sqrt(N) worst cases for 1 <= x <= N
        In general it's O(logx)
        */

        public int PrimePalindrome2(int N)
        {
            if (8 <= N && N <= 11) return 11;
            for (int x = 1; x < 100000; x++)
            {
                string s = x.ToString();
                char[] charArray = s.ToCharArray();
                Array.Reverse(charArray);
                string r = new string(charArray);
                int y = int.Parse(s + r.Substring(1));
                if (y >= N && IsPrime(y)) return y;
            }
            return -1;
        }

        // This is a good way to check prime numbers!!!
        public bool IsPrime(int x)
        {
            if (x < 2 || x % 2 == 0) return x == 2;  // if it is even number, we return false, for even number, only 2 is prime number
            // sqrt(x) times at most, check all odd number up to sqrt(x),for even number, we dont need to check even number for i, because if it can be divided by an even number, then itself must be a even number.
            // all numbers pass first line, are odd number, it won't be divided by even number, there is no meaning to check even numbers  here, the x % i will always be false for even number i.
            for (int i = 3; i * i <= x; i += 2) 
                if (x % i == 0) return false;
            return true;
        }

        // Way to construct palindrome numbers in order
        public int PrimePalindrome3(int N)
        {
            for (int L = 1; L <= 5; ++L)
            {
                //Check for odd-length palindromes
                for (int root = (int)Math.Pow(10, L - 1); root < (int)Math.Pow(10, L); ++root)
                {
                    StringBuilder sb = new StringBuilder(root.ToString());
                    for (int k = L - 2; k >= 0; --k)
                        sb.Append(sb[k]);
                    int x = int.Parse(sb.ToString());
                    if (x >= N && IsPrime2(x))
                        return x;
                    //If we didn't check for even-length palindromes:
                    //return N <= 11 ? Math.Min(x, 11) : x;
                }

                //Check for even-length palindromes, we know even length is not needed, skip the rest
                for (int root = (int)Math.Pow(10, L - 1); root < (int)Math.Pow(10, L); ++root)
                {
                    StringBuilder sb = new StringBuilder(root.ToString());
                    for (int k = L - 1; k >= 0; --k)
                        sb.Append(sb[k]);
                    int x = int.Parse(sb.ToString());
                    if (x >= N && IsPrime2(x))
                        return x;
                }
            }

            throw null;
        }

        public bool IsPrime2(int N)
        {
            if (N < 2) return false;
            int R = (int)Math.Sqrt(N);
            for (int d = 2; d <= R; ++d)
                if (N % d == 0) return false;
            return true;
        }
    }


}
