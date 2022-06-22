using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class Paypal
    {
        public static List<int> reassignShelves(List<int> shelves)
        {
            //1,12,4,12
            var ans = new List<int>();
            var avail = new List<int>(shelves);
            avail.Sort();
            //1,4,12,12
            var dict = new Dictionary<int, int>();
            for (var i = 0; i < avail.Count; i++)
            {
                if (!dict.ContainsKey(avail[i]))
                {
                    dict.Add(avail[i], i + 1);
                }

            }
            for (var i = 0; i < shelves.Count; i++)
            {
                ans.Add(dict[shelves[i]]);
            }

            return ans;
        }

        public static string IsSimilarOrder(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                return "NO";


            // can use array to do counting as well
            //var count1 = new Dictionary<char, int>();
            //var count2 = new Dictionary<char, int>();


            var count1 = new int[26];
            var count2 = new int[26];

            foreach(char c in s1)
            {
                count1[c-'a']++;
            }

            foreach(char c in s2)
            {
                count2[c - 'a']++;
            }

            for(int i = 0; i < 26; i++)
            {
                if (count1[i] > 0 && count2[i] > 0)
                {
                    int delta = Math.Abs(count1[i] - count2[i]);
                    if (delta > 3)
                        return "NO";
                }
            }

            return "YES";
        }


        public static List<string> userLog(string[] logs, int max)
        { 
            var result = new List<string>();
            // time or (time, isSign) boolean
            // Dictionary<string, int> map = new Dictionary<string, int>();
            Dictionary<string, (int, bool)> map2 = new Dictionary<string, (int, bool)>();
            foreach(string log in logs)
            {
                string[] items = log.Split(' ');
                string userId = items[0];
                int currentTime = int.Parse(items[1]);
                //string sign = items[2];
                bool isSignIn = items[2] == "sign-in";

                if(!map2.ContainsKey(userId))
                {
                    map2.Add(userId, (currentTime, isSignIn));
                }
                else
                {
                    var val = map2[userId];
                    if(val.Item2 && !isSignIn)
                    {
                        int diff = currentTime - val.Item1;
                        map2[userId] = (diff, isSignIn);
                    }
                }
            }

            foreach(var kvp in map2)
            {
                if(kvp.Value.Item1 <= max && !kvp.Value.Item2)
                {
                    result.Add(kvp.Key);
                }
            }

            result.Sort();

            return result;
        }

    }
}
