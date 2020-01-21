using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] test = new int[4][];
            test[0] = new int[] { 1};
            test[1] = new int[] {2};
            test[2] = new int[] { 1 };
            test[3] = new int[] { 1 };


            //KClosestPoints solu = new KClosestPoints();
            //int[][] res = solu.KClosest(test, 2);

            //[[1],[2],[1],[2]]

            RottingOrange so = new RottingOrange();
            int res = so.ServerProblem(4, 1, test);

            Console.WriteLine("Hello World!");
        }
    }
}
