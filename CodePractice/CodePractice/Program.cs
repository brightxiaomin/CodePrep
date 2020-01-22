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
            test[0] = new int[] { 1 };
            test[1] = new int[] { 2 };
            test[2] = new int[] { 1 };
            test[3] = new int[] { 1 };


            //KClosestPoints solu = new KClosestPoints();
            //int[][] res = solu.KClosest(test, 2);

            //[[1],[2],[1],[2]]

            //RottingOrange so = new RottingOrange();
            //int res = so.ServerProblem(4, 1, test);

            string[] toys = { "elmo", "elsa", "legos", "drone", "tablet", "Warcraft" };

            string[] quotes = 
          { "Elmo is the hottest of the season! Elmo will be on every kid's wishlist!",
          "The new Elmo dolls are super high quality",
          "Expect the Elsa dolls to be very popular this year, Elsa!",
          "Elsa and Elmo are the toys I'll be buying for my kids, Elsa is good",
          "For parents of older kids, look into buying them a drone",
          "Warcraft is slowly rising in popularity ahead of the holiday season "};

            var res = new TopNToysClean().TopToys(6, 3, toys, 6, quotes);

            Console.WriteLine("Hello World!");
        }
    }
}
