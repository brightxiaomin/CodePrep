using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class MaxNumberOfEngineer
    {
        // max number of engineers
        int MaxTeams(int teamSize, int maxDiff, int[] skills)
        {
            if (teamSize == 1)
                return skills.Length;
            Array.Sort(skills);
            int teams = 0;
            int leastSkilled = 0;
            for (int i = 1; i < skills.Length; ++i)
            {
                while (skills[i] - skills[leastSkilled] > maxDiff)
                    ++leastSkilled;
                if (i - leastSkilled + 1 == teamSize)
                {
                    ++teams;
                    leastSkilled = i + 1;
                }
            }
            return teams;
        }
    }
}
