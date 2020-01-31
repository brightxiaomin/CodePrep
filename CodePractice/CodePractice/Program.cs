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
            #region old test
            //int[][] test = new int[4][];
            //test[0] = new int[] { 1 };
            //test[1] = new int[] { 2 };
            //test[2] = new int[] { 1 };
            //test[3] = new int[] { 1 };


            //var test3 = Console.ReadLine();

            //string[] ad = test3.Split(',');



            //KClosestPoints solu = new KClosestPoints();
            //int[][] res = solu.KClosest(test, 2);

            //[[1],[2],[1],[2]]

            //RottingOrange so = new RottingOrange();
            //int res = so.ServerProblem(4, 1, test);

            //  string[] toys = { "elmo", "elsa", "legos", "drone", "tablet", "Warcraft" };

            //  string[] quotes = 
            //{ "Elmo is the hottest of the season! Elmo will be on every kid's wishlist!",
            //"The new Elmo dolls are super high quality",
            //"Expect the Elsa dolls to be very popular this year, Elsa!",
            //"Elsa and Elmo are the toys I'll be buying for my kids, Elsa is good",
            //"For parents of older kids, look into buying them a drone",
            //"Warcraft is slowly rising in popularity ahead of the holiday season "};

            //  var res = new TopNToysClean().TopToys(6, 3, toys, 6, quotes);


            //int numRouters = 7;
            //int numLinks = 7;
            //int[][] links = new int[][] { new int[] { 0, 1 },
            //     new int[]{ 0, 2 },  new int[]{ 1, 3 },  new int[]{ 2, 3 },  new int[]{ 2, 5 },  new int[]{ 5, 6 },  new int[]{ 3, 4 } };

            //var res = new CriticialRouters().GetCriticalNodes(links, numLinks, numRouters);
            //char c = 'c';
            //int s = c - 'a';

            //List<string> tt = new List<string> { "mouse", "hello", "easy" };

            //tt.Sort();

            //tt.RemoveAt(tt.Count - 1);
            #endregion

            EightQueen eightQueen = new EightQueen();
            eightQueen.CalculateQueen(0);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
