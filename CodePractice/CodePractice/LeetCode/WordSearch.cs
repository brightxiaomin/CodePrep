using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    class WordSearch
    {
        //I have some idea about it, using DFS, I know how to solve with one start node
        //but this one have all possible starting node, so the actually search, is to try every possible
        // starting node, I am trying to optimize at very beginning, no need. just get the answer first.
        //DFS mix with backtrack (normal DFS, terminating is reaching boundary, while backtrack, it is not found a solution, not valid step)
        public bool Exist(char[][] board, string word)
        {
            //start from all nodes
            for(int i = 0; i < board.Length; i++)
            {
                for(int j  =0; j < board[0].Length; j++)
                {
                    // anytime it is found, method return.
                    if (DFS(board, word, i, j, 0)) return true;
                }
            }

            return false;
        }

        // there is a bug, cannot reuse letter inside one try
        private bool DFS(char[][] board, string word, int row, int col, int pos)
        {
            // found
            if (pos == word.Length) return true;
            // out of grid
            if (row < 0 || row >= board.Length || col < 0 || col >= board[0].Length)
                return false;

            char temp = board[row][col];
            if (temp != word[pos]) return false;

            // make sure same node is not reused
            board[row][col] = '*';

            // run four directions, if first true, second not exectued. 
           bool exist=  DFS(board, word, row + 1, col, pos + 1) || DFS(board, word, row - 1, col, pos + 1)
                 || DFS(board, word, row, col + 1, pos + 1) || DFS(board, word, row, col - 1, pos + 1);

            //this the point of backtrack, you go back, and another route, this node can be reused now
            board[row][col] = temp;

            return exist;
        }


        //word search II, kind of brutal force
        public IList<string> FindWords(char[][] board, string[] words)
        {
            List<string> ret = new List<string>();
            foreach (string word in words)
            {
                if (Exist(board, word))
                    ret.Add(word);
            }
            return ret;
        }
    }
}
