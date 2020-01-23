using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class MinStepsTreasureIslands
    {
		public  int TreasureIsland(char[][] island)
		{
			if (island == null || island.Length == 0) return 0;

			int steps = 0;
			Queue<int[]> queue = new Queue<int[]>();
			queue.Enqueue(new int[] { 0, 0 });

			//bool[][] visited = new bool[island.Length][];
			//for (int i = 0; i < island.Length; i++)
			//	visited[0] = new bool[island[0].Length];
			//visited[0][0] = true;

			//mark starting point as visited
			island[0][0] = 'D';

			int[][] dirs = new int[][]
			{
				new int[] { 1, 0 },
				new int[] { -1, 0 },
				new int[] { 0, 1 },
				new int[] { 0, -1 }
			};

			// bfs
			while (queue.Count > 0)
			{
				int size = queue.Count;
				for (int i = 0; i < size; i++)
				{
					int[] point = queue.Dequeue();
					int r = point[0];
					int c = point[1];
					if (island[r][c] == 'X') return steps;

					foreach (int[] dir in dirs)
					{
						int newR = point[0] + dir[0];
						int newC = point[1] + dir[1];

						if (newR >= 0 && newR < island.Length && newC >= 0 && newC < island[0].Length &&
								island[newR][newC] != 'D' )
						{
							queue.Enqueue(new int[] { newR, newC });
							//mark visited by changing to 'D'
							island[newR][newC] = 'D';
						}
					}
				}
				steps++;
			}

			return -1;


		}
	}
}
