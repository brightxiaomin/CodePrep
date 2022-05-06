using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class SmallestStringRemovingOneChar
    {
        // Function to return the
        // smallest String by removing on character
        static string smallest(string s)
        {
            int l = s.Length;
            String ans = "";

            // iterate the String
            for (int i = 0; i < l - 1; i++)
            {

                // first point where s[i]>s[i+1]
                if (s[i] > s[i + 1])
                {

                    // append the String without
                    // i-th character in it
                    for (int j = 0; j < l; j++)
                    {
                        if (i != j)
                        {
                            ans += s[j];
                        }
                    }
                    return ans;
                }
            }

            // leave the last character
            ans = s.Substring(0, l - 1);
            return ans;
        }
    }
}
