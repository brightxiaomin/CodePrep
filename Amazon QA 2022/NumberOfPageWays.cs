using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class NumberOfPageWays
    {
        // Count the number of pages 
        public long NumberOfWays(string s)
        {
            long ans = 0;
            int len = s.Length;

            long totOnes = 0;
            for (int i = 0; i < len; i++)
                totOnes += s[i] - '0';//only s[i]=='1' plus 1

            long totZeros = len - totOnes;
            long currZeros = s[0] == '0' ? 1 : 0;
            long currOnes = s[0] == '1' ? 1 : 0;

            for (int i = 1; i < len; i++)
            {
                if (s[i] == '0')
                {
                    ans += (currOnes * (totOnes - currOnes));
                    currZeros++;
                }
                else
                {
                    ans += (currZeros * (totZeros - currZeros));
                    currOnes++;
                }
            }
            return ans;
        }
    }
}
