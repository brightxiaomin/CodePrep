using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class WordLadderII
    {
        private int minLevel = int.MaxValue;
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            int level = 0;
            IList<IList<string>> res = new List<IList<string>>();
            HashSet<string> remainSet = new HashSet<string>(wordList);
            HashSet<string> route = new HashSet<string>() { beginWord };
            BackTrack(beginWord, endWord, remainSet, route, res, level);
            return res;
        }


        //backtrack, when to return, more than min level or hit endword.
        //word list is the word left for word
        void BackTrack(string beginWord, string endWord, HashSet<string> remainSet, HashSet<string> route, IList<IList<string>> res, int le)
        {
            if (le > minLevel) return;

            //found endword here
            if (beginWord == endWord)
            {

                if (le < minLevel)
                {
                    minLevel = le;
                    res.Clear();
                    res.Add(route.ToList());
                    return;
                }
                else if (le == minLevel)
                {
                    res.Add(route.ToList());
                    return;
                }

            }

            int len = beginWord.Length;

            for (int j = 0; j < len; j++) //O(L * 26)
            {
                //or this way using stringbuilder
                StringBuilder sb = new StringBuilder(beginWord);
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    if (ch == beginWord[j]) continue;
                    sb[j] = ch;
                    string temp = sb.ToString();
                    if (remainSet.Remove(temp))  //in set and removed
                    {
                        route.Add(temp);
                        BackTrack(temp, endWord, remainSet, route, res, le + 1);
                        route.Remove(temp);
                        remainSet.Add(temp);
                    }
                }
            }

        }

        public IList<IList<string>> FindLadders2(string start, string end, IList<string> wordList)
        {
            HashSet<string> dict = new HashSet<string>(wordList);
            IList<IList<string>> res = new List<IList<string>>();
            Dictionary<string, List<string>> nodeNeighbors = new Dictionary<string, List<string>>();// Neighbors for every node
            Dictionary<string, int> distance = new Dictionary<string, int>();// Distance of every node from the start node
            List<string> solution = new List<string>();

            dict.Add(start);
            Bfs(start, end, dict, nodeNeighbors, distance);
            Dfs(start, end, dict, nodeNeighbors, distance, solution, res);
            return res;
        }

        // BFS: Trace every node's distance from the start node (level by level).
        private void Bfs(string start, string end, HashSet<string> dict, Dictionary<string, List<string>> nodeNeighbors, Dictionary<string, int> distance)
        {
            foreach (string str in dict)
                nodeNeighbors.Add(str, new List<string>());

            Queue<string> queue = new Queue<string>();
            queue.Enqueue(start);
            distance.Add(start, 0);

            while (queue.Count > 0)
            {
                int count = queue.Count;
                bool foundEnd = false;
                for (int i = 0; i < count; i++)
                {
                    string cur = queue.Dequeue();
                    int curDistance = distance[cur];
                    List<string> neighbors = getNeighbors(cur, dict);

                    foreach (string neighbor in neighbors)
                    {
                        nodeNeighbors[cur].Add(neighbor);
                        if (!distance.ContainsKey(neighbor))
                        {// Check if visited
                            distance.Add(neighbor, curDistance + 1);
                            if (end.Equals(neighbor))// Found the shortest path
                                foundEnd = true;
                            else
                                queue.Enqueue(neighbor);
                        }
                    }
                }

                if (foundEnd)
                    break;
            }
        }

        // Find all next level nodes.    
        private List<string> getNeighbors(string node, HashSet<string> dict)
        {
            List<string> res = new List<string>();
            char[] chs = node.ToCharArray();

            for (int i = 0; i < chs.Length; i++)
            {
                for (char ch = 'a'; ch <= 'z'; ch++)
                {
                    if (chs[i] == ch) continue;
                    char old_ch = chs[i];
                    chs[i] = ch;
                    string temp = new string(chs);
                    if (dict.Contains(temp))
                    {
                        res.Add(temp);
                    }
                    chs[i] = old_ch;
                }

            }
            return res;
        }

        // DFS: output all paths with the shortest distance.
        private void Dfs(string cur, string end, HashSet<string> dict, Dictionary<string, List<string>> nodeNeighbors, Dictionary<string, int> distance, List<string> solution, IList<IList<string>> res)
        {
            solution.Add(cur);
            if (end.Equals(cur))
            {
                res.Add(new List<string>(solution));
            }
            else
            {
                foreach (string next in nodeNeighbors[cur])

                    if (distance[next] == distance[cur] + 1)
                    {
                        Dfs(next, end, dict, nodeNeighbors, distance, solution, res);
                    }
            }
            solution.RemoveAt(solution.Count - 1);
        }



        //A sample solution for C sharp
        // two end BFS
        // very smilar logic as this java implementations
        // https://leetcode.com/problems/word-ladder-ii/discuss/40477/Super-fast-Java-solution-(two-end-BFS)
        public class SolutionTwoBFS
        {
            // create a dictionary for parent and children relations for one step switching
            // use a bidirectional bfs to create the dictionary, and its parents/children relationships. This will keep going until one word is found within the end set.
            // use a DFS backtracking method to create the possible lists for the shortest transformations

            public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
            {
                HashSet<string> set = new HashSet<string>(wordList);
                IList<IList<string>> list = new List<IList<string>>();
                if (!set.Contains(endWord))
                {
                    return list;
                }
                Dictionary<string, List<string>> dct = getChildren(beginWord, endWord, set);
                IList<string> path = new List<string>();
                path.Add(beginWord);
                findLadder(beginWord, endWord, dct, path, list);
                return list;
            }

            public Dictionary<string, List<string>> getChildren(string beginWord, string endWord, HashSet<string> wordBank)
            {
                Dictionary<string, List<string>> dct = new Dictionary<string, List<string>>();
                HashSet<string> beginSet = new HashSet<string>();
                HashSet<string> endSet = new HashSet<string>();
                beginSet.Add(beginWord);
                endSet.Add(endWord);
                HashSet<string> visited = new HashSet<string>();
                bool isBackward = false;
                bool isFound = false;

                // loop can rewrite to recursion.
                //visited is shared, 
                while (beginSet.Count > 0 && !isFound)
                {
                    if (beginSet.Count > endSet.Count)
                    {
                        isBackward = !isBackward;
                        HashSet<string> temp = beginSet;
                        beginSet = endSet;
                        endSet = temp;
                    }
                    HashSet<string> set = new HashSet<string>();
                    foreach (string cur in beginSet)
                    {
                        visited.Add(cur);
                        foreach (string next in getNext(cur, wordBank))
                        {
                            if (visited.Contains(next) || beginSet.Contains(next))
                            {
                                continue;
                            }
                            if (endSet.Contains(next))
                            {
                                isFound = true;
                            }
                            set.Add(next);
                            string parent = isBackward ? next : cur;
                            string child = isBackward ? cur : next;
                            if (!dct.ContainsKey(parent))
                            {
                                dct.Add(parent, new List<string>());
                            }
                            dct[parent].Add(child);
                        }
                    }
                    beginSet = set;
                }
                return dct;
            }

            public void findLadder(string beginWord, string endWord, Dictionary<string, List<string>> dct, IList<string> path, IList<IList<string>> res)
            {
                if (beginWord == endWord)
                {
                    res.Add(new List<string>(path));
                }
                if (!dct.ContainsKey(beginWord))
                {
                    return;
                }
                IList<string> list = dct[beginWord];
                foreach (string s in list)
                {
                    path.Add(s);
                    findLadder(s, endWord, dct, path, res);
                    path.RemoveAt(path.Count - 1);
                }
            }

            public IList<string> getNext(string word, HashSet<string> wordBank)
            {
                IList<string> list = new List<string>();
                char[] cArr = word.ToCharArray();
                for (int i = 0; i < cArr.Length; i++)
                {
                    char old = cArr[i];
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == old)
                        {
                            continue;
                        }
                        cArr[i] = c;
                        string s = new string(cArr);
                        if (wordBank.Contains(s))
                        {
                            list.Add(s);
                        }
                    }
                    cArr[i] = old;
                }
                return list;
            }

        }
    }

}
