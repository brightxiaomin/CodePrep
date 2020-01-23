using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class RottingOrange
    {
        public int OrangesRotting(int[][] grid)
        {
            //edge case
            if (grid == null || grid.Length == 0)
                return 0;

            //define direction array to code cleaness
            int[][] dir = new int[4][]
            {
                new int[] { 1, 0 },
                new int[] { -1, 0 },
                new int[] { 0, 1 },
                new int[] { 0, -1 }
            };

            int rows = grid.Length;
            int cols = grid[0].Length;
            Queue<int[]> queue = new Queue<int[]>();
            int count_fresh = 0;
            int time = 0;

            // two for loop
            for(int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i][j] == 2)
                        queue.Enqueue(new int[] { i, j }); // add index of rotten orange to queue
                    else if (grid[i][j] == 1)
                        count_fresh++;
                }
            }

            //if no fresh orange
            if (count_fresh == 0) return 0;

            //BFS
            while (queue.Count > 0)
            {
                time++;
                int size = queue.Count;
                // take out all elements in the queue, represent the rotten organge at time x
                // can not use queue.count, it is changed every time
                for(int i = 0; i < size; i++)
                {
                    int[] point = queue.Dequeue();
                    foreach(int[] dr in dir)
                    {
                        int r = point[0] + dr[0];
                        int c = point[1] + dr[1];
                        if (r < 0 || c < 0 || r >= rows || c >= cols || grid[r][c] != 1) 
                            continue;
                        grid[r][c] = 2;
                        queue.Enqueue(new int[] { r, c });
                        count_fresh--;
                    }
                }
            }
            return count_fresh == 0 ? time - 1 : -1;
        }


        //only '1' and '0'
        public int ServerProblem(int rows, int cols, int[][] grid)
        {
            //edge case
            if (grid == null || grid.Length == 0)
                return 0;

            //define direction array for code cleaness
            int[][] dir = new int[4][]
            {
                new int[] { 1, 0 },
                new int[] { -1, 0 },
                new int[] { 0, 1 },
                new int[] { 0, -1 }
            };

            Queue<int[]> queue = new Queue<int[]>();
            int count_old = 0;
            int days = 0;

            // two for loop
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i][j] == 1)
                        queue.Enqueue(new int[] { i, j }); // add index of rotten orange to queue
                    else if (grid[i][j] == 0)
                        count_old++;
                }
            }

            //if no fresh orange
            //if (count_fresh == 0) return 0;

            //BFS
            while (queue.Count > 0 && count_old > 0)
            {
                days++;
                int size = queue.Count;
                // take out all elements in the queue, represent the rotten organge at time x
                // can not use queue.count, it is changed every time
                for (int i = 0; i < size; i++)
                {
                    int[] point = queue.Dequeue();
                    foreach (int[] dr in dir)
                    {
                        int r = point[0] + dr[0];
                        int c = point[1] + dr[1];
                        if (r < 0 || c < 0 || r >= rows || c >= cols || grid[r][c] == 1)
                            continue;
                        grid[r][c] = 1;
                        queue.Enqueue(new int[] { r, c });
                        count_old--;
                    }
                }
            }
            return count_old == 0 ? days : -1;
        }
    }
}
