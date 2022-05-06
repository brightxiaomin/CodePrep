using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class MaxLengthConcateString
    {
        private int result = 0;

        public int MaxLength(IList<string> arr)
        {
            if (arr == null || arr.Count == 0)
            {
                return 0;
            }

            dfs(arr, "", 0);

            return result;
        }

        private void dfs(IList<string> arr, string path, int idx)
        {
            bool isUniqueChar = IsUniqueChars(path);

            if (isUniqueChar)
            {
                result = Math.Max(path.Length, result);
            }

            if (idx == arr.Count || !isUniqueChar)
            {
                return;
            }

            for (int i = idx; i < arr.Count; i++)
            {
                if (!IsUniqueChars(arr[i])) continue;
                dfs(arr, path + arr[i], i + 1);
            }
        }

        private bool IsUniqueChars(string s)
        {
            HashSet<char> set = new HashSet<char>();

            foreach (char c in s)
            {
                if (set.Contains(c))
                {
                    return false;
                }
                set.Add(c);
            }
            return true;
        }

        bool isUniqueChars(string s)
        {
            bool[] set = new bool[26];
            foreach (char c in s)
            {
                if (set[c - 'a']) return false;
                set[c - 'a'] = true;
            }
            return true;
        }
    }
}
