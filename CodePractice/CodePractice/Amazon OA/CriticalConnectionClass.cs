using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class CriticalConnectionClass
    {
        // Basically, it uses dfs to travel through the graph to find if current vertex u, can travel back to u or previous vertex
        //low[u] records the lowest vertex u can reach
        //disc[u] records the time when u was discovered

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            int[] low = new int[n];
            // use adjacency list instead of matrix will save some memory, adjmatrix will cause MLE
            List<int>[] graph = new List<int>[n];
            List<IList<int>> res = new List<IList<int>>();

            int[] disc = Enumerable.Repeat<int>(-1, n).ToArray();  // use disc to track if visited (disc[i] == -1)

            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            // build graph
            for (int i = 0; i < connections.Count; i++)
            {
                int from = connections[i][0], to = connections[i][1];
                graph[from].Add(to);
                graph[to].Add(from);
            }

            for (int i = 0; i < n; i++)
            {
                if (disc[i] == -1)
                {
                    DFS(i, low, disc, graph, res, i);
                }
            }
            return res;
        }

        int time = 0; // time when discover each vertex

        private void DFS(int u, int[] low, int[] disc, List<int>[] graph, List<IList<int>> res, int pre)
        {
            disc[u] = low[u] = ++time; // discover u
            for (int j = 0; j < graph[u].Count; j++)
            {
                int v = graph[u][j];
                if (v == pre)
                {
                    continue; // if parent vertex, ignore
                }
                if (disc[v] == -1)
                { // if not discovered
                    DFS(v, low, disc, graph, res, u);
                    low[u] = Math.Min(low[u], low[v]);
                    if (low[v] > disc[u])
                    {
                        // u - v is critical, there is no path for v to reach back to u or previous vertices of u
                        res.Add(new List<int> { u, v });
                    }
                }
                else
                { // if v discovered and is not parent of u, update low[u], cannot use low[v] because u is not subtree of v
                    low[u] = Math.Min(low[u], disc[v]);
                }
            }
        }
    }
}
