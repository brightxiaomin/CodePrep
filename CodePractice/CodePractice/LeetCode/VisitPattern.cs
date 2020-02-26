using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    struct Pair
    {
        public int time;
        public string web;
        public Pair(int time, string web)
        {
            this.time = time;
            this.web = web;
        }
    }
    class Solution2
    {
        public List<string> MostVisitedPattern(string[] username, int[] timestamp, string[] website)
        {
            Dictionary<string, List<Pair>> map = new Dictionary<string, List<Pair>>();
            int n = username.Length;
            // collect the website info for every user, key: username, value: (timestamp, website)
            for (int i = 0; i < n; i++)
            {
                if (!map.ContainsKey(username[i])) map.Add(username[i], new List<Pair> ());
                map[username[i]].Add(new Pair(timestamp[i], website[i]));
            }
            // count map to record every 3 combination occuring time for the different user.
            Dictionary<string, int> count = new Dictionary<string, int>();
            string res = "";
            foreach (string key in map.Keys)
            {
                HashSet<string> set = new HashSet<string>();
                // this set is to avoid visit the same 3-seq in one user
                List<Pair> list = map[key];
                list.Sort((a, b) => (a.time - b.time)); // sort by time
                                                                   // brutal force O(N ^ 3)
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        for (int k = j + 1; k < list.Count; k++)
                        {
                            string str = list[i].web + " " + list[j].web + " " + list[k].web;
                            if (!set.Contains(str))
                            {
                                if (!count.ContainsKey(str)) count.Add(str, 0);
                                count[str] = count[str] + 1;
                                set.Add(str);
                            }
                            if (res.Equals("") || count[res] < count[str] || (count[res] == count[str] && res.CompareTo(str) > 0))
                            {
                                // make sure the right lexi order
                                res = str;
                            }
                        }
                    }
                }
            }
            // grab the right answer
            string[] r = res.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            foreach (string str in r)
            {
                result.Add(str);
            }
            return result;
        }

        public string LongestDupSubstring(string S)
        {
            int n = S.Length;

            int[] nums = new int[n];
            for (int i = 0; i < n; ++i) nums[i] = S[i] - 'a';

            // base value for the rolling hash function
            int a = 26;
            // modulus value for the rolling hash function to avoid overflow
            long modulus = (long)Math.Pow(2, 32);
            int left = 1, right = n;
            while (left <= right)
            {
                int L = left + (right - left) / 2;
                if (Search(L, a, modulus, n, nums) != -1) left = L + 1;
                else right = L - 1;
            }

            int index = Search(left - 1, a, modulus, n, nums);

            return S.Substring(index, left - 1);
        }

        public int Search(int L, int a, long modulus, int n, int[] nums)
        {
            // compute the hash of string S[0:L], the first one in the rolling hash
            long h = 0;
            for (int i = 0; i < L; ++i) h = (h * a + nums[i]) % modulus;

            // already seen hashes of strings of length L
            HashSet<long> seen = new HashSet<long>();
            seen.Add(h);

            // const value to be used often : a**L % modulus
            long aL = 1;
            for (int i = 1; i <= L; ++i) aL = (aL * a) % modulus;


            for (int start = 1; start < n - L + 1; ++start)
            {
                // compute rolling hash in O(1) time
                //h = (h * a - nums[start - 1] * aL % modulus + modulus) % modulus;
                //h = (h + nums[start + L - 1]) % modulus;
                // compute rolling hash in O(1) time
                h = (h * a - nums[start - 1] * aL + nums[start + L - 1]) % modulus;
                if (seen.Contains(h)) return start;
                seen.Add(h);
            }
            return -1;
        }

    }
}
