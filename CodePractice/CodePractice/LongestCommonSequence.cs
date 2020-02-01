using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class LongestCommonSequence
    {
        /* CLRS book present and prove the theorem
         * C[i,j] = C[i-1, j -1] + 1; if Xi = Yj
         * C[i,j] = Max(C[i-1, j], C[i, j -1]); if Xi <> Yj
         * C[i,j] = 0; if i ==0 or j == 0
         */

        //create the decision array to reconstuct the sequence
        private char[,] decision;


        //reproduce the algorithm on the book
        // here just return length
        // the book needs to return the table len and decision
        //time: O(M*N)
        //space: O(M*N)
        public int GetLCSLength(string s1, string s2)
        {
            int m = s1.Length, n = s2.Length;

            //construct the two tables
            //Len array to store the solution at i,j
            //Decision array to store which spot it was picked in the previous subproblem, this way, we can reconstruct the sequence.

            //use two-d array for simplicity, jagged array is faster, but has to do for loop (array of array)
            // len size is (m+1) * (n+1), sentinel approach to simplify code
            int[,] len = new int[m + 1, n + 1];
            decision = new char[m, n];

            //we do row-major order, fill in first row, then second row

            //fill first column for len array, starting at 1, because below one take care of 0,0
            for (int i = 1; i <= m; i++)
                len[i, 0] = 0;

            //fill first row
            for (int i = 0; i <= n; i++)
                len[0, i] = 0;

            //start from 1,1 which is first character for each string
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        // two things
                        //calculate vale for len[i,j]
                        len[i, j] = len[i - 1, j - 1] + 1;
                        //record the decision at i,j
                        decision[i - 1, j - 1] = 'D';  // D for diagnol
                    }
                    else
                    {
                        //from up
                        // note, > or >= are both okay, the longest length is unique, just one number, 
                        // but the path to reach longest are not necesarily only one, could be multiple

                        if (len[i - 1, j] >= len[i, j - 1])
                        {
                            //take up
                            len[i, j] = len[i - 1, j];
                            //record
                            decision[i - 1, j - 1] = 'U'; // U for up
                        }
                        else
                        {
                            //take up, if same, we take Left
                            len[i, j] = len[i, j - 1];
                            //record
                            decision[i - 1, j - 1] = 'L'; // U for up
                        }
                    }
                }
            }

            return len[m, n];
        }

        //Print LCS reverse order
        //loop through decision,
        public int PrintLCSRevers(string s1)
        {
            int m = decision.GetLength(0), n = decision.GetLength(1);
            int count = 0;

            //this loop is wrong
            //for(int i = m - 1; i >= 0 && j>=0;)
            //{
            //    for(int j = n -1; i >= 0 && j >=0; )
            //    {
            //        count++;
            //        Console.Write(decision[i, j]);
            //        if(decision[i,j] == 'D')
            //        {
            //            //if means s1[i] == s2[j]
            //            // print the current one
            //            //Console.Write(s1[i]);
            //            //both i and j needs to be updated
            //            i = i - 1; j = j - 1;
            //        }
            //        else if(decision[i, j] == 'U')
            //        {
            //            //updte row
            //            i = i - 1;
            //        }
            //        else
            //        {
            //            // no if needed, as it must be 'L'
            //            j = j - 1;
            //        }

            //    }

            //}

            int i = m - 1, j = n - 1;
            while (i >= 0 && j >= 0)
            {
                count++;
                //Console.Write(decision[i, j]);
                if (decision[i, j] == 'D')
                {
                    //if means s1[i] == s2[j]
                    // print the current one
                    Console.Write(s1[i]);
                    //both i and j needs to be updated
                    i--; j--;
                }
                else if (decision[i, j] == 'U')
                {
                    //updte row
                    i--;
                }
                else
                {
                    // no if needed, as it must be 'L'
                    j--;
                }
            }
            Console.WriteLine();
            return count;
        }


        //print in normal order,
        //need to user recursion

        public void PrintLCS(string s1, int i, int j)
        {
            if (i < 0 || j < 0)  // terminating condition for the recursion, stack
                return;

            if (decision[i, j] == 'D')
            {
                //go to up left
                PrintLCS(s1, i - 1, j - 1); // be cautious, --i or i++ in the parameter, it not only pass the new value and change the value for variable i.
                Console.Write(s1[i]);
            }
            else if (decision[i, j] == 'U')
            {
                //go up
                PrintLCS(s1, i - 1, j);
            }
            else
            {
                //go left
                PrintLCS(s1, i, j - 1);
            }
        }


        //print decision matrix for debugging
        public void PrintDecisionMatrix()
        {
            int m = decision.GetLength(0), n = decision.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(decision[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
