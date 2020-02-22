using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    class WordBreak2
    {
        public static IList<string> WordBreak(string s, IList<string> wordDict)
        {
            return DFS(s, wordDict, new Dictionary<string, LinkedList<string>>());
        }

        /// <summary>
        /// DFS function returns an array including all substrings derived from s.
        /// I need to get comfortable to use Double Linked List class LinkedList class
        /// Also I need to push myself to put together a memoization plan when I try to apply DFS search. 
        /// Focus on more using string class API like StartsWith, same as Java class. 
        /// LinkedList is a good choice to manage the collection in-between given function argument and final result. 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="wordDict"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private static IList<string> DFS(string s, IList<string> wordDict, Dictionary<string, LinkedList<string>> map)
        {
            // Look up cache 
            if (map.ContainsKey(s))
            {
                return map[s].ToList();
            }

            var list = new LinkedList<string>();

            // base case 
            if (s.Length == 0)
            {
                list.AddLast("");
                return list.ToList();
            }

            // go over each word in dictionary
            foreach (string word in wordDict)
            {
                if (s.StartsWith(word))
                {
                    var sublist = DFS(s.Substring(word.Length), wordDict, map);

                    foreach (string sub in sublist)
                    {
                        list.AddLast(word + (string.IsNullOrEmpty(sub) ? "" : " ") + sub);
                    }
                }
            }

            // memoization
            map.Add(s, list);

            return list.ToList();
        }

        // dont understand why use linked list, a little faster than list solution
        public List<string> WordBreak4(string s, IList<string> wordDict)
        {
            // no meaning for this one
            int n = s.Length;
            HashSet<string> set = new HashSet<string>(wordDict);

            // Check if there is at least one possible sentence
            bool[] dp1 = new bool[n + 1];
            dp1[0] = true;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (dp1[j] && set.Contains(s.Substring(j, i - j)))
                    {
                        dp1[i] = true;
                        break;
                    }
                }
            }

            // We are done if there isn't a valid sentence at all
            if (!dp1[n])
            {
                return new List<string>();
            }

            //interstingly that the above code snnipt solves the TLE problem

            LinkedList<string>[] dp = new LinkedList<string>[n + 1];
            LinkedList<string> initial = new LinkedList<string>();
            initial.AddLast("");
            dp[0] = initial;
            for (int i = 1; i <= s.Length; i++)
            {
                LinkedList<string> list = new LinkedList<string>();
                for (int j = 0; j < i; j++)
                {
                    if (dp[j].Count > 0 && set.Contains(s.Substring(j, i - j)))
                    {
                        foreach (string l in dp[j])
                        {
                            list.AddLast(l + (string.IsNullOrEmpty(l) ? "" : " ") + s.Substring(j, i-j));
                        }
                    }
                }
                dp[i] = list;
            }
            return dp[n].ToList();
        }


        //use list, TLE
        public List<string> WordBreak3(string s, IList<string> wordDict)
        {
            int n = s.Length;
            HashSet<string> set = new HashSet<string>(wordDict);
            List<string>[] dp = new List<string>[n + 1];
            List<string> initial = new List<string>();
            initial.Add("");
            dp[0] = initial;
            for (int i = 1; i <= s.Length; i++)
            {
                List<string> list = new List<string>();
                for (int j = 0; j < i; j++)
                {
                    if (dp[j].Count > 0 && set.Contains(s.Substring(j, i - j)))
                    {
                        foreach (string l in dp[j])
                        {
                            list.Add(l + (string.IsNullOrEmpty(l) ? "" : " ") + s.Substring(j, i - j));
                        }
                    }
                }
                dp[i] = list;
            }
            return dp[n];
        }
    }

    public class TT
    {

        private Dictionary<string, List<string>> cache = new Dictionary<string, List<string>>();

        public List<string> WordBreak(string s, IList<string> wordDict)
        {
            return WordBreak(s, new HashSet<string>(wordDict));
        }
        private bool containsSuffix(HashSet<string> dict, string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (dict.Contains(str.Substring(i))) return true;
            }
            return false;
        }

        public List<string> WordBreak(string s, HashSet<string> dict)
        {
            if (cache.ContainsKey(s)) return cache[s];
            List<string> result = new List<string>();
            if (dict.Contains(s)) result.Add(s);
            for (int i = 1; i < s.Length; i++)
            {
                string left = s.Substring(0, i), right = s.Substring(i);
                if (dict.Contains(left) && containsSuffix(dict, right))
                {
                    foreach (string ss in WordBreak(right, dict))
                    {
                        result.Add(left + " " + ss);
                    }
                }
            }
            cache.Add(s, result);
            return result;
        }
    }
}
    

