using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class Questions
    {
        // Optimal Utilization
        public List<int[]> GetPairs(List<int[]> a, List<int[]> b, int target)
        {
            a.Sort((i, j) => i[1] - j[1]);
            b.Sort((i, j) => i[1] - j[1]);
            List<int[]> result = new List<int[]>();
            int max = int.MinValue;
            int m = a.Count;
            int n = b.Count;
            int i = 0;
            int j = n - 1;
            while (i < m && j >= 0)
            {
                int sum = a[i][1] + b[j][1];
                if (sum > target)
                {
                    --j;
                }
                else
                {
                    if (max <= sum)
                    {
                        if (max < sum)
                        {
                            // we got even closer pair 
                            max = sum;
                            result.Clear();
                        }
                        result.Add(new int[] { a[i][0], b[j][0] });
                        int index = j - 1;
                        while (index >= 0 && b[index][1] == b[index + 1][1])
                        {
                            result.Add(new int[] { a[i][0], b[index][0] });
                            index--;
                        }
                    }
                    ++i;
                }
            }
            return result;
        }


        // valid string, similar as valid parenthesis
        public bool IsValid(String s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                if (stack.Count == 0 || c != stack.Peek())
                {
                    stack.Push(c);
                }
                else if (c == stack.Peek())
                {
                    stack.Pop();
                }
            }

            return stack.Count == 0;

        }

        // Given an array of only 1 and -1, find a subarray of maximum length such that the product of all the elements in the subarray is 1
        // iterate the array, store position of all -1,if count is even number,return whole array, else compare head to last -1, and tail to first 1


        // decreasing review list, decreasing sublist
        // monotonic decrease
        public static int DecreasingSubarray(int[] array)
        {
            Stack<int> stack = new Stack<int>();
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (stack.Count > 0 && stack.Peek() <= array[i])
                {
                    stack.Clear();
                }
                stack.Push(array[i]);
                count += stack.Count;
            }
            return count;
        }

    }

    class Solution
    {

        private static  int[,] directions =
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
            bool[,] visited = new bool [grid.Length, grid[0].Length]; // Used as visited set.
            visited[0,0] = true;

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
                int newRow = row + directions[i,0];
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
