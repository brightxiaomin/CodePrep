using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Billboards
    {
       static void Print()
        {
            string input = Console.ReadLine();
            int result = GetRejectNumber(input);
            Console.WriteLine(result.ToString());
        }

        static int GetRejectNumber(string input)
        {
            int result = 0;

            if (string.IsNullOrWhiteSpace(input))
                return result;

            string[] requests = input.Split(',');

            //array to store the occupied info for each billboard
            bool[] occupied = new bool[100];

            for(int i = 0; i < requests.Length; i++)
            {
                int num = int.Parse(requests[i]);
                if (num > 100 || occupied[num - 1])
                    result++;
                else
                    occupied[num - 1] = true;
            }

            return result;
        }
    }
}
