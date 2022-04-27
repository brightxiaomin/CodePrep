using CodePractice.LeetCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Program
    {
        static void Main()
        {
            #region old test

            // TOP N toy problem
            // string[] toys = { "elmo", "elsa", "legos", "drone", "tablet", "Warcraft" };
            // string[] quotes = 
            //{ "Elmo is the hottest of the season! Elmo will be on every kid's wishlist!",
            //"The new Elmo dolls are super high quality",
            //"Expect the Elsa dolls to be very popular this year, Elsa!",
            //"Elsa and Elmo are the toys I'll be buying for my kids, Elsa is good",
            //"For parents of older kids, look into buying them a drone",
            //"Warcraft is slowly rising in popularity ahead of the holiday season "};
            //  var res = new TopNToysClean().TopToys(6, 3, toys, 6, quotes);


            //Critical Routers
            //int numRouters = 7;
            //int numLinks = 7;
            //int[][] links = new int[][] { new int[] { 0, 1 },
            //     new int[]{ 0, 2 },  new int[]{ 1, 3 },  new int[]{ 2, 3 },  new int[]{ 2, 5 },  new int[]{ 5, 6 },  new int[]{ 3, 4 } };
            //var res = new CriticialRouters().GetCriticalNodes(links, numLinks, numRouters);


            //EightQueen eightQueen = new EightQueen();
            //eightQueen.CalculateQueen(0);


            //LCS
            //string a = "ABCBDAB", b = "BDCABA";
            //var test = new LongestCommonSequence();
            //int res = test.GetLCSLengthUsingOneRow(a, b);
            //Console.WriteLine("length: " + res);
            //test.PrintDecisionMatrix();
            //int count = test.PrintLCSRevers(a);
            //Console.WriteLine("count is " + count);
            //test.PrintLCS(a, 6, 5);



            //var test = new Knapsack();
            ////test.CalculateMaxUsingMemo(0, 0);
            //int res = test.GetMaxDP();

            //[5,9,3,2,1,0,2,3,3,1,0,0]
            // Mine: -2147483647
            //expected: 3
            //int[] test = { 5, 9, 3, 2, 1, 0, 2, 3, 3, 1, 0, 0 };
            //var res = new JumpGame().JumpGreedy(test);
            //var res = new SlidingWindowSubstrings().LengthOfLongestSubstring("abcabcbb");

            //string m = "aguokepatgbnvfqmgmlcupuufxoohdfpgjdmysgvhmvffcnqxjjxqncffvmhvgsymdjgpfdhooxfuupuculmgmqfvnbgtapekouga";
            //string s = "eeccccbebaeeabebccceea";
            //string s = "abca";
            //string s = "aguokepatgbnvfqmgmlcupuufxoohdfpgjdmysgvhmvffcnqxjjxqncffvmhvgsymdjgpfdhooxfuupuculmgmqfvnbgtapekouga";
            //string s = "abc";

            //Console.WriteLine(m == s);
            //var res = new Palindrome().ValidPalindrome(s);
            //Console.WriteLine(res);

            //var res = new SlidingWindowSubstrings().LengthOfLongestSubstring("abcdefgcx");
            //Console.WriteLine(res);


            //List<string> test = new List<string> { "hot", "dot", "dog", "lot", "log", "cog" };
            //var res = new WordLadderII().FindLadders2("hit", "cog", test);

            //["i"],[" "],["a"],["#"],["i"],[" "],["a"],["#"]]

            //string[] input = { "i love you", "island", "iroman", "i love leetcode" };
            //int[] num = { 5, 3, 2, 2 };
            //var aut = new AutocompleteSystem(input, num);
            //var r = aut.Input('i');
            //var b = aut.Input(' ');
            //var c = aut.Input('a');
            //var d = aut.Input('#');
            //var tt = aut.Input('i');
            //var bb = aut.Input(' ');
            //var cc = aut.Input('a');
            //var dd = aut.Input('#');
            //var rf = aut.Input('i');
            //var bbd = aut.Input(' ');
            //var cdc = aut.Input('a');
            //var ddd = aut.Input
            #endregion

            string s = "rabbbit", t = "rabbit";

            int ans = NumDistinct(s, t);

            Console.ReadLine();
          
           
        }

        public string SimplifyPath(string path)
        {
            string[] dirs = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            Stack<string> st = new Stack<string>();
            foreach(string dir in dirs)
            {
                if (dir == ".")
                    continue;
                else if(dir == "..")
                {
                    if (st.Count > 0) st.Pop();
                } 
                else
                {
                    st.Push(dir);
                }
            }

            //StringBuilder sb = new StringBuilder();
            //while(st.Count > 0)
            //{
            //    sb.Insert(0, st.Pop());
            //    sb.Insert(0, '/');
            //}

            //return sb.Length == 0 ? "/" : sb.ToString();

            List<string> result = new List<string>();
            while (st.Count > 0)
            {
                result.Insert(0, st.Pop());
            }
            return "/" + string.Join("/", result);

        }


        public static int NumDistinct(string s, string t)
        {
            int m = s.Length, n = t.Length;


            // dp[i][j] means distinct subsequence for s[0...i-1] and t[0...j-1]
            int[][] dp = new int[m + 1][];
            for (int i = 0; i <= m; i++)
                dp[i] = new int[n + 1];

            for (int i = 0; i <= m; i++)
                dp[i][0] = 1; // empty string is a sub of any string

            for (int i = 0; i < m; i++)
                for (int j = 0; i < n; j++)
                {
                    if (s[i] == t[j])
                        dp[i + 1][j + 1] = dp[i][j] + dp[i][j + 1]; // match on both or skip source
                    else
                        dp[i + 1][j + 1] = dp[i][j + 1];

                }

            return dp[m][n];
        }

        //compare diag
        public void SearchMatrix(int[,] matrix, int target)
        {
            int rows = matrix.GetLength(0), cols = matrix.GetLength(1);

            int lr, lc, rr = rows - 1, rc = cols - 1;

            if( rows > cols)
            {
                lr = rows - cols;
                lc = 0;
            }
            else
            {
                lr = 0;
                lc = cols - rows;
            }

            int left = 0, right = Math.Min(rows, cols) - 1;
         

            //boundary define
            while(left < right)
            {
                int mid = (left + right) / 2;
                if (target > matrix[lr + mid, lc + mid])
                {
                    left = mid + 1;
                }
                else
                    right = mid;
            }
        }

        public string MostCommonWord(string paragraph, string[] banned)
        {
            string[] p = paragraph.ToLower().Split(new char[] { ' ', ',', ';', '!', '?', '.', '\''});
            Dictionary<string, int> map = new Dictionary<string, int>();
            HashSet<string> set = new HashSet<string>(banned);
            int max = int.MinValue;
            string ans = string.Empty;
            foreach (string w in p)
            {
                if (!set.Contains(w))
                {
                    if (!map.ContainsKey(w))
                        map.Add(w, 1);
                    else
                        map[w] = map[w] + 1;

                    if (map[w] > max)
                    {
                        max = map[w];
                        ans = w;
                    }
                }
            }
            return ans;

        }


        public void Practice()
        {


            string s = "test";
            string t = "test";
            //256 array
            int[] map = new int[256];
            foreach (char c in t)
                map[c - 'a']++;

            int left = 0, right = 0, counter = t.Length;
            while (right < s.Length)
            {
                //decrement the char
                map[s[right] - 'a']--;
                //means we have it before
                if (map[s[right] - 'a'] > 0) ; counter--; // repeating char, val > 1;count++
                right++;

                while (counter == 0)
                {
                    map[s[left] - 'a']++;
                    if (map[s[right] - 'a'] > 0) counter++;
                    left++;

                // do something,  == length or get min
                }

                // get max here

            }


            Stack<int> stack = new Stack<int>();
            stack.Push(3);
            stack.Pop();
            stack.Peek();
            //return res;
            //test company pc git connection
        }
    }
}
