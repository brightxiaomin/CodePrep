﻿using CodePractice.LeetCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Program
    {
        static void Main()
        {
            #region old test

            // TOP N toy problem
            // string[] toys = { "elmo", "elsa", "legos", "drone", "tablet", "Warcraft" };
            // string[] quotes = 
            //{ "Elmo is the hottest of the season! Elmo will be on every kid's wishlist!",
            //"The new Elmo dolls are super high quality",
            //"Expect the Elsa dolls to be very popular this year, Elsa!",
            //"Elsa and Elmo are the toys I'll be buying for my kids, Elsa is good",
            //"For parents of older kids, look into buying them a drone",
            //"Warcraft is slowly rising in popularity ahead of the holiday season "};
            //  var res = new TopNToysClean().TopToys(6, 3, toys, 6, quotes);


            //Critical Routers
            //int numRouters = 7;
            //int numLinks = 7;
            //int[][] links = new int[][] { new int[] { 0, 1 },
            //     new int[]{ 0, 2 },  new int[]{ 1, 3 },  new int[]{ 2, 3 },  new int[]{ 2, 5 },  new int[]{ 5, 6 },  new int[]{ 3, 4 } };
            //var res = new CriticialRouters().GetCriticalNodes(links, numLinks, numRouters);


            //EightQueen eightQueen = new EightQueen();
            //eightQueen.CalculateQueen(0);


            //LCS
            //string a = "ABCBDAB", b = "BDCABA";
            //var test = new LongestCommonSequence();
            //int res = test.GetLCSLengthUsingOneRow(a, b);
            //Console.WriteLine("length: " + res);
            //test.PrintDecisionMatrix();
            //int count = test.PrintLCSRevers(a);
            //Console.WriteLine("count is " + count);
            //test.PrintLCS(a, 6, 5);
            #endregion


            //var test = new Knapsack();
            ////test.CalculateMaxUsingMemo(0, 0);
            //int res = test.GetMaxDP();

            //[5,9,3,2,1,0,2,3,3,1,0,0]
            // Mine: -2147483647
            //expected: 3
            //int[] test = { 5, 9, 3, 2, 1, 0, 2, 3, 3, 1, 0, 0 };
            //var res = new JumpGame().JumpGreedy(test);
            //var res = new SlidingWindowSubstrings().LengthOfLongestSubstring("abcabcbb");

            //string m = "aguokepatgbnvfqmgmlcupuufxoohdfpgjdmysgvhmvffcnqxjjxqncffvmhvgsymdjgpfdhooxfuupuculmgmqfvnbgtapekouga";
            //string s = "eeccccbebaeeabebccceea";
            //string s = "abca";
            //string s = "aguokepatgbnvfqmgmlcupuufxoohdfpgjdmysgvhmvffcnqxjjxqncffvmhvgsymdjgpfdhooxfuupuculmgmqfvnbgtapekouga";
            string s = "abc";

            //Console.WriteLine(m == s);
            var res = new Palindrome().ValidPalindrome(s);
            Console.WriteLine(res);
            Console.ReadLine();
        }
    }
}
