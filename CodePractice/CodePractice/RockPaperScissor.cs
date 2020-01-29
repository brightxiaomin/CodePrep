using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class RockPaperScissor
    {
        //beat rules, value beats key
        //rock beat scissor
        //paper beat rock
        //scissor beat paper
        private static Dictionary<char, char> beatRules = new Dictionary<char, char>
        {
            {'S', 'R' },
            {'R', 'P' },
            {'P', 'S' },
        };

        public static int GetWinRounds(string input)
        {
            int result = 0;

            //process first two rounds, 
            for(int i = 0; i < 2 && i < input.Length; i++)
            {
                if (Win('R', input[i]) > 0)
                    result++;
            }

            //process rest of characters
            for(int i = 2; i < input.Length; i++)
            {
                char myNext = MyNextMove(input[i - 2], input[i - 1]);
                if (Win(myNext, input[i]) > 0)
                    result++;
            }

            return result;
        }

        //apply psychology, based on opponent two previous move
        //decide my next move
        private static char  MyNextMove(char prepre, char pre)
        {
            List<char> threeMoves = new List<char> { 'R', 'S', 'P' };

            if (prepre == pre) return beatRules[pre];

            threeMoves.Remove(prepre);
            threeMoves.Remove(pre);

            char next = threeMoves.First();

            return beatRules[next];
        }


        private static int Win(char c1, char c2)
        {
            //c1 beats c2
            if (c1 == beatRules[c2]) return 1;
            //c2 beats c1
            if (c2 == beatRules[c1]) return -1;

            //tie
            return 0;
        }
    }
}
