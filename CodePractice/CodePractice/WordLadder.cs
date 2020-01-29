using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class WordLadder
    {
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            //got some hint, can use BFS
            //from begin word, one letter change, a list of words, like one step, one level
            // on that list, change one letter, another list, like another level
            // when when hit the target, we return the number of steps.
            // it is very similar like the remove obstacle problem


            if (!wordList.Contains(endWord))
                return 0;

            int res = 0;
            int len = wordList.Count;
            Queue<string> queue = new Queue<string>();

            bool[] visited = new bool[len];

            queue.Enqueue(beginWord);

            while (queue.Count > 0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    string word = queue.Dequeue();

                    //as long as we get the target, we return the value, res result level
                    if (word == endWord) return res;

                    for (int j = 0; j < len; j++)
                    {
                        if (!visited[j] && DifferByOneLetter(word, wordList[j]))
                        {
                            visited[j] = true;
                            queue.Enqueue(wordList[j]);
                        }
                    }
                }
                res++;
            }

            return 0;

        }


        //O(M) operations, M is word length
        public bool DifferByOneLetter(string s1, string s2)
        {
            int count = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == s2[i]) continue;
                count++;
                if (count > 1)
                    return false;
            }

            return true;
        }
    }
}
