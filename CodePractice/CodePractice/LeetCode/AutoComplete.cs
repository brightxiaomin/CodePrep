using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class AutocompleteSystem
    {

        private string sent;
        private TrieNode root;
        public AutocompleteSystem(string[] sentences, int[] times)
        {
            //build the trie
            root = new TrieNode();
            for (int i = 0; i < sentences.Length; i++)
            {
                TrieNode current = root;
                string se = sentences[i];
                foreach (char c in se)
                {
                    if (c == ' ')
                    {
                        if (current.Nodes[26] == null) current.Nodes[26] = new TrieNode();
                        current = current.Nodes[26];
                        continue;
                    }

                    if (current.Nodes[c - 'a'] == null) current.Nodes[c - 'a'] = new TrieNode();
                    current = current.Nodes[c - 'a'];
                }
                current.Count += times[i];
                current.word = se;
            }
        } 

        public IList<string> Input(char d)
        {
            List<string> res = new List<string>();
            //end of sentence
            if (d == '#')
            {
                //insert sent to trie
                TrieNode current = root;
                foreach (char c in sent)
                {
                    if (c == ' ')
                    {
                        if (current.Nodes[26] == null) current.Nodes[26] = new TrieNode();
                        current = current.Nodes[26];
                        continue;
                    }

                    if (current.Nodes[c - 'a'] == null) current.Nodes[c - 'a'] = new TrieNode();
                    current = current.Nodes[c - 'a'];
                }
                current.word = sent;
                current.Count++;
                sent = "";
            }
            else
            {
                List<TrieNode> temp = new List<TrieNode>();
                sent += d;
                //search the whole tree
                TrieNode current = root;
                foreach (char c in sent)
                {
                    current = c == ' ' ? current.Nodes[26] : current.Nodes[c - 'a'];
                    if (current == null) break;
                    //;
                }
                if (current.Count > 0) temp.Add(current);
                //last node in sent
                if (current != null)
                {
                    foreach (var item in current.Nodes)
                    {
                        DFS(item, temp);
                    }

                    temp.Sort((a, b) => a.Count == b.Count ? a.word.CompareTo(b.word) : b.Count - a.Count);

                    int len = Math.Min(3, temp.Count);
                    for (int i = 0; i < len; i++)
                        res.Add(temp[i].word);
                }
        }

            return res;
        }

        private void DFS(TrieNode root, List<TrieNode> ans)
        {
            if (root == null) return;
            foreach (var item in root.Nodes)
            {
                DFS(item, ans);
            }
            if (root.Count > 0) ans.Add(root);
        }

    }

    public class TrieNode
    {
        public TrieNode[] Nodes;
        public int Count;
        public string word;
        public TrieNode()
        {
            Count = 0;
            Nodes = new TrieNode[27];
        }
    }

    /**
     * Your AutocompleteSystem object will be instantiated and called as such:
     * AutocompleteSystem obj = new AutocompleteSystem(sentences, times);
     * IList<string> param_1 = obj.Input(c);
     */


    // This is another approach, each trie node has the list of words (sentence) that go through 
    public class AutocompleteSystem2
    {
        private readonly TrieNode2 root;

        private readonly Dictionary<string, int> map;

        private TrieNode2 curNode;

        private StringBuilder curSb;

        public AutocompleteSystem2(string[] sentences, int[] times)
        {
            root = new TrieNode2();
            curNode = root;
            curSb = new StringBuilder();
            map = new Dictionary<string, int>();

            for (int i = 0; i < sentences.Length; i++)
            {
                string curSent = sentences[i];
                int curTimes = times[i];
                var node = root;
                map.Add(curSent, curTimes);
                foreach (char ch in curSent)
                {
                    int charIndex = ch == ' ' ? 26 : ch - 'a';
                    if (node.children[charIndex] == null)
                    {
                        node.children[charIndex] = new TrieNode2();
                    }

                    node = node.children[charIndex];
                    OrderFrequentSentences(node, curSent);
                }
            }
        }

        public IList<string> Input(char c)
        {
            if (c == '#')
            {
                string sent = curSb.ToString();
                AddSentence(sent);
                curNode = root;
                curSb = new StringBuilder();
                return new List<string>();
            }
            else
            {
                curSb.Append(c);
                int charIndex = c == ' ' ? 26 : c - 'a';

                if (curNode == null || curNode.children[charIndex] == null)
                {
                    curNode = null;
                    return new List<string>();
                }

                curNode = curNode.children[charIndex];
                return curNode.mostFrequentSents;
            }
        }

        private void AddSentence(string sent)
        {
            map[sent] = map.ContainsKey(sent) ? map[sent] + 1 : 1;
            var node = root;
            foreach (char ch in sent)
            {
                int charIndex = ch == ' ' ? 26 : ch - 'a';
                if (node.children[charIndex] == null)
                {
                    node.children[charIndex] = new TrieNode2();
                }

                node = node.children[charIndex];
                OrderFrequentSentences(node, sent);
            }
        }

        private void OrderFrequentSentences(TrieNode2 node, string sent)
        {
            if (!node.mostFrequentSents.Contains(sent))
            {
                node.mostFrequentSents.Add(sent);
            }

            node.mostFrequentSents.Sort((a, b) => map[a] == map[b]
                ? string.Compare(a, b, StringComparison.Ordinal)
                : map[b] - map[a]);

            if (node.mostFrequentSents.Count > 3)
            {
                node.mostFrequentSents.RemoveAt(3);
            }
        }

        private class TrieNode2
        {
            public readonly List<string> mostFrequentSents;

            public readonly TrieNode2[] children;

            public TrieNode2()
            {
                mostFrequentSents = new List<string>();
                children = new TrieNode2[27];
            }
        }
    }

    /**
     * Your AutocompleteSystem object will be instantiated and called as such:
     * AutocompleteSystem obj = new AutocompleteSystem(sentences, times);
     * IList<string> param_1 = obj.Input(c);
     */

}
