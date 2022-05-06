using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class LargestNumberSoBothExist
    {
        // Largest value of K such that both K and -K exist in Array in given index range [L, R]
        public static int FindMax(int N, int[] arr, int L, int R)
        {
            // Using a set to store the elements
            HashSet<int> s = new HashSet<int>();

            // ArrayList to store the possible answers
            List<int> possible = new List<int>();

            // Store the answer
            int ans = 0;

            // Traverse the array
            for (int i = 0; i < N; i++)
            {
                // If set has it's negation,
                // check if it is max
                if (s.Contains(arr[i] * -1))
                    possible.Add(Math.Abs(arr[i]));
                else
                    s.Add(arr[i]);
            }

            // Find the maximum possible answer
            for (int i = 0; i < possible.Count; i++)
            {
                if (possible[i] >= L && possible[i] <= R)
                {
                    ans = Math.Max(ans, possible[i]);
                }
            }

            return ans;
        }


        // Function to find the Largest Alphabetic char
        public static String largestchar(String str)
        {

            // Array for keeping track of both uppercase
            // and lowercase english alphabets
            bool[] uppercase = new bool[26];
            bool[] lowercase = new bool[26];

            char[] arr = str.ToCharArray();

            foreach (char c in arr)
            {
                if (char.IsLower(c))
                    lowercase[c - 'a'] = true;

                if (char.IsUpper(c))
                    uppercase[c - 'A'] = true;
            }

            // Iterate from right side of array
            // to get the largest index character
            for (int i = 25; i >= 0; i--)
            {

                // Check for the character if both its
                // uppercase and lowercase exist or not
                if (uppercase[i] && lowercase[i])
                    return (char)(i + 'A') + "";
            }

            // Return -1 if no such character whose
            // uppercase and lowercase present in
            // string str
            return "-1";

        }
    }
}
