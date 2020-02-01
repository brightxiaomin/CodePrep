using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class CriticialRouters
    {
		// this method is similar to https://www.geeksforgeeks.org/articulation-points-or-cut-vertices-in-a-graph/
		// https://www.geeksforgeeks.org/tarjan-algorithm-find-strongly-connected-components/

		private int time = 0;
        public List<int> GetCriticalNodes(int[][] links, int numLinks, int numRouters)
        {
			time = 0;
			Dictionary<int, HashSet<int>> map = new Dictionary<int, HashSet<int>>();
			for (int i = 0; i < numRouters; i++)
			{
				map.Add(i, new HashSet<int>());
			}

			// each node, store its neighbor nodes in the hashset
			// in the dictionary
			foreach (int[] link in links)
			{
				map[link[0]].Add(link[1]);
				map[link[1]].Add(link[0]);
			}
			
			HashSet<int> set = new HashSet<int>();
			int[] low = new int[numRouters];
			int[] dis = Enumerable.Repeat<int>(-1, numRouters).ToArray(); //also server the purpose of visited[] array purpose. if not -1, mean visited.
			int[] parent = Enumerable.Repeat<int>(-1, numRouters).ToArray();


			for (int i = 0; i < numRouters; i++)
			{
				if (dis[i] == -1)
					DFS(map, low, dis, parent, i, set);
			}

			return set.ToList();
		}

        private void DFS(Dictionary<int, HashSet<int>> map, int[] low, int[] dis, int[] parent, int cur, HashSet<int> res)
        {
			int children = 0;
			dis[cur] = low[cur] = ++time;
			foreach (int ne in map[cur])
			{
				if (dis[ne] == -1)
				{
					children++;
					parent[ne] = cur;
					DFS(map, low, dis, parent, ne, res);

					low[cur] = Math.Min(low[cur], low[ne]);

					//#1. current is root of DFS tree and has two or more chilren.
					// #2. If u is not root and low value of one of its child is more than discovery value of u.
					if ((parent[cur] == -1 && children > 1) || (parent[cur] != -1 && low[ne] >= dis[cur]))
						res.Add(cur);
				}
				else if (ne != parent[cur])
					low[cur] = Math.Min(low[cur], dis[ne]);
			}

		}
    }
}
