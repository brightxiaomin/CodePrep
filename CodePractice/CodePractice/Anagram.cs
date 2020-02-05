using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    using System.Collections.Generic;
    public class Solution
    {

        //this more like two pointers.
        public IList<int> FindAnagramsUsingDictionary(string s, string p)
        {
            //sliding window approach, two pointers, one counter
            List<int> result = new List<int>();
            //Dictionary to store the frequency of each characters
            //more generic way
            Dictionary<char, int> freq = new Dictionary<char, int>();
            for (int i = 0; i < p.Length; i++)
            {
                if (freq.ContainsKey(p[i]))
                    freq[p[i]] = freq[p[i]] + 1;
                else
                    freq.Add(p[i], 1);
            }

            //two pointers, one counter
            int left = 0, right = 0, counter = freq.Count;

            while (right < s.Length)
            {
                if (freq.ContainsKey(s[right]))
                {
                    freq[s[right]] = freq[s[right]] - 1; //minus one

                    //check if one character in the dictionary is all covered
                    if (freq[s[right]] == 0)
                        counter--;
                }
                right++;

                //next need to move left pointer
                // this whil loop is to move left all the way to skip 
                // characters not in p, and extra characters from p in s
                while (counter == 0)
                {
                    if (freq.ContainsKey(s[left]))
                    {
                        // move pass one character, right side needs to cover this character
                        // so plus one 
                        freq[s[left]] = freq[s[left]] + 1;
                        //this check is necessary as the value could be still zero
                        // > 0 means reset to zero,
                        if (freq[s[left]] > 0)
                            counter++;
                    }

                    // all chacters covered plus same length thats a hit
                    if (right - left == p.Length)
                        result.Add(left);

                    //always move left
                    left++;
                }
            }

            return result;
        }


        //this is real sliding window, first run is build window with p.Length, then slide
        // right pointer moves one, left pointer moves one step
        public IList<int> FindAnagramsUsingArray(string s, string p)
        {
            List<int> result = new List<int>();

            //we assume the string only contains lower case letters
            int[] freq = new int[26];

            //populate the array
            for (int i = 0; i < p.Length; i++)            
                freq[p[i] - 'a']++;            

            //two pointers, one counter
            int left = 0, right = 0, counter = p.Length;

            while(right < s.Length)
            {
                //if freq array has it before
                //means we need to cover it
                if(freq[s[right] - 'a'] > 0)
                    counter--;

                freq[s[right] - 'a']--; //always minus 1
                right++;

                if (counter == 0) result.Add(left);

                //move left pointer if right -left == p.len
                if(right - left == p.Length)
                {
                    if (freq[s[left] - 'a'] >= 0) // going to skip a, count plus one
                        counter++;

                    freq[s[left] - 'a']++; //always plus one
                    left++;
                }
            }

            return result;
        }
    }
}
