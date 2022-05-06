using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class MinDeletionsToMakeFrequencyUnique
    {
        public int MinDeletions(string s)
        {
            int[] ct = new int[26];

            foreach (char c in s)
                ct[c - 'a']++;

            Array.Sort(ct);
            int del = 0;
            int maxFreqAllowed = s.Length;

            for (int i = 25; i >= 0 && ct[i] > 0; i--)
            {
                // delete characters to make the frequence equal the max allowed freq
                if (ct[i] > maxFreqAllowed)
                {
                    del += ct[i] - maxFreqAllowed;
                    ct[i] = maxFreqAllowed;
                }

                // update next max allowed freq to one smaller than current freq
                maxFreqAllowed = Math.Max(0, ct[i] - 1);
            }

            return del;
        }
    }
}
