using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class SlidingWindowSubstrings
    {
        /* Can solove 76. Minimum Window Substring
         * 3. Longest Substring Without Repeating Characters
         * longest-substring-with-at-most-two-distinct-characters
         * Longest Substring with At Most K Distinct Characters
         * 30. Substring with Concatenation of All Words
         * 438. Find All Anagrams in a String
         * 567. Permutation in String
         * Longest Repeating Character Replacement
         */

        //Template for sliding window
        public List<int> SlidingTemplate(string s, string t)
        {
            //init a collection or int value to save the result according the question.
            List<int> result = new List<int>();
            if (t.Length > s.Length) return result;

            //create a hashmap to save the Characters of the target substring.
            //(K, V) = (Character, Frequence of the Characters)
            Dictionary<char, int> map = new Dictionary<char, int>();
            foreach (char c in t.ToCharArray())
            {
                if (map.ContainsKey(c))
                    map[c] = map[c] + 1;
                else
                    map.Add(c, 1);
            }

            //maintain a counter to check whether match the target string.
            int counter = map.Count;//must be the map size, NOT the string size because the char may be duplicate.

            //Two Pointers: begin - left pointer of the window; end - right pointer of the window
            int begin = 0, end = 0;

            //the length of the substring which match the target string.
            //int len = int.MaxValue;

            //loop at the begining of the source string
            while (end < s.Length)
            {

                char c = s[end];//get a character

                if (map.ContainsKey(c))
                {
                    map[c] = map[c] - 1;// plus or minus one
                    if (map[c] == 0) counter--;//modify the counter according the requirement(different condition).
                }
                end++;

                //increase begin pointer to make it invalid/valid again
                while (counter == 0 /* counter condition. different question may have different condition */)
                {

                    char tempc = s[begin];//***be careful here: choose the char at begin pointer, NOT the end pointer
                    if (map.ContainsKey(tempc))
                    {
                        map[tempc] = map[tempc] - 1;//plus or minus one
                        if (map[tempc] > 0) counter++;//modify the counter according the requirement(different condition).
                    }

                    /* save / update(min/max) the result if find a target*/
                    // result collections or result int value

                    begin++;
                }

                //Max len type of problem sometimes get updated here
            }
            return result;
        }

        //Template two
        //int findSubstring(string s)
        //{
        //    int[] map = new int[128];
        //    int counter; // check whether the substring is valid
        //    int begin = 0, end = 0; //two pointers, one point to tail and one  head
        //    int d; //the length of substring

        //    for () { /* initialize the hash map here */ }

        //    while (end < s.size())
        //    {

        //        if (map[s[end++]]-- ?) {  /* modify counter here */ }

        //        if (map[s[end] > 0])
        //            map[end] = map[end] - 1;
        //        end++;

        //        while (/* counter condition */)
        //        {

        //            /* update d here if finding minimum*/

        //            //increase begin to make it invalid/valid again

        //            if (map[s[begin++]]++ ?) { /*modify counter here*/ }
        //            if (map[s[begin] > 0])
        //                map[begin] = map[begin] + 1;
        //            begin++;
        //        }

        //        /* update d here if finding maximum*/
        //    }
        //    return d;
        //}

        public IList<int> FindAnagrams(string s, string p)
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
                while (counter == 0)
                {
                    if (freq.ContainsKey(s[left]))
                    {
                        // move pass one character, right side needs to cover this character
                        // so plus one 
                        //reset to characters in p
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

        public IList<int> FindAnagramsTwo(string s, string p)
        {
            List<int> result = new List<int>();

            //we assume the string only contains lower case letters
            int[] freq = new int[26];

            //populate the array
            for (int i = 0; i < p.Length; i++)
                freq[p[i] - 'a']++;

            //two pointers, one counter
            int left = 0, right = 0, counter = p.Length;

            while (right < s.Length)
            {
                //if freq array has it before
                //means we need to cover it
                if (freq[s[right] - 'a'] > 0)
                    counter--;

                freq[s[right] - 'a']--; //always minus 1
                right++;

                if (counter == 0) result.Add(left);

                //move left pointer if right -left == p.len
                if (right - left == p.Length)
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
