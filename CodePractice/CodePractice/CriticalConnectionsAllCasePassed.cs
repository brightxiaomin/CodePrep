using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Solution
    {
        List<List<int>> list;
        Dictionary<int, bool> visited;
        List<List<int>> GetCriticalConnections(int numOfServers, int numOfConnections, List<List<int>> connections)
        {
            Dictionary<int, HashSet<int>> adj = new Dictionary<int, HashSet<int>>();
            foreach (List<int> connection in connections)
            {
                int u = connection[0];
                int v = connection[1];

                if (!adj.ContainsKey(u))
                    adj.Add(u, new HashSet<int>());
                adj[u].Add(v);

                if (!adj.ContainsKey(v))
                    adj.Add(v, new HashSet<int>());
                adj[v].Add(u);
            }

                list = new List<List<int>>();
            for (int i = 0; i < numOfConnections; i++)
            {
                visited = new Dictionary<int, bool>();
                List<int> p = connections[i];
                int x = p[0];
                int y = p[1];
                adj[x].Remove(y);
                adj[y].Remove(x);
                DFS(adj, 1);
                if (visited.Count != numOfServers)
                {
                    if (p[0] > p[1])
                        list.Add(new List<int> { p[1], p[0] });
                    else
                        list.Add(p);
                }
                adj[x].Add(y);
                adj[y].Add(x);
            }
            return list;
        }

        public void DFS(Dictionary<int, HashSet<int>> adj, int u)
        {
            visited.Add(u, true);
            if (adj[u].Count != 0)
            {
                foreach (int v in adj[u])
                {
                    if (!visited.ContainsKey(v) || !visited[v])
                    {
                        DFS(adj, v);
                    }
                }
            }
        }
    }


}


