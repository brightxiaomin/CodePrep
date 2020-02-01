using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class EightQueen
    {
        private int[] result = new int[8];
        private int total = 0;

        public void CalculateQueen(int row)
        {
            //accept, return to output
            if(row == 8)
            {
                total++;
                //printQueen method
                PrintQueens();

                return;
            }

            for(int column = 0; column < 8; column++) // first candidate is row and column =0, then next, is row, column+ 1 so on.
            {
                if(IsOk(row, column))  // if not okay, then reject and return
                {
                    result[row] = column;
                    CalculateQueen(row + 1); // back tracking point. 
                }
            }
        }

        private bool IsOk(int row, int column)
        {
            int leftUp = column - 1, rightUp = column + 1;
            //iterate previous rows
            for(int i = row - 1; i>=0; i--)
            {
                //Constraint rules logic implemented here
                // if there is any queen on the same column up, not ok
                if (result[i] == column) return false;

                //diagonal, first is row-1, col -1, goes up one level, row -2, col-2
                if (leftUp >= 0 && result[i] == leftUp) return false;

                if (rightUp < 8 && result[i] == rightUp) return false;

                leftUp--;
                rightUp++;
            }

            return true;
        }


        private void PrintQueens()
        {
            for(int row = 0; row < 8; row++)
            {
                for(int column = 0; column < 8; column++)
                {
                    if (result[row] == column)
                        Console.Write("Q ");
                    else
                        Console.Write("* ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(total);
        }

       
    }
}
