using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    // Shortest path in binary matrix
    class ShortestsPathBinaryMatrix
    {
        private static int[,] directions =
            new int[,] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };

        public int ShortestPathBinaryMatrix(int[][] grid)
        {

            // Firstly, we need to check that the start and target cells are open.
            if (grid[0][0] != 0 || grid[grid.Length - 1][grid[0].Length - 1] != 0)
            {
                return -1;
            }

            // Set up the BFS.
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { 0, 0, 1 }); // Put distance on the queue
            bool[,] visited = new bool[grid.Length, grid[0].Length]; // Used as visited set.
            visited[0, 0] = true;

            // Carry out the BFS
            while (queue.Count > 0)
            {
                int[] cell = queue.Dequeue();
                int row = cell[0];
                int col = cell[1];
                int distance = cell[2];
                // Check if this is the target cell.
                if (row == grid.Length - 1 && col == grid[0].Length - 1)
                {
                    return distance;
                }
                foreach (int[] neighbour in GetNeighbours(row, col, grid))
                {
                    int neighbourRow = neighbour[0];
                    int neighbourCol = neighbour[1];
                    if (visited[neighbourRow, neighbourCol])
                    {
                        continue;
                    }
                    visited[neighbourRow, neighbourCol] = true;
                    queue.Enqueue(new int[] { neighbourRow, neighbourCol, distance + 1 });
                }
            }

            // The target was unreachable.
            return -1;
        }

        private List<int[]> GetNeighbours(int row, int col, int[][] grid)
        {
            List<int[]> neighbours = new List<int[]>();
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newRow = row + directions[i, 0];
                int newCol = col + directions[i, 1];
                if (newRow < 0 || newCol < 0 || newRow >= grid.Length
                        || newCol >= grid[0].Length
                        || grid[newRow][newCol] != 0)
                {
                    continue;
                }
                neighbours.Add(new int[] { newRow, newCol });
            }
            return neighbours;
        }
    }
}
