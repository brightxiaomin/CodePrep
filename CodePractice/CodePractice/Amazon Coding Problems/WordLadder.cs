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

            int res = 1;
            int len = wordList.Count;
            Queue<string> queue = new Queue<string>();

            bool[] visited = new bool[len];

            queue.Enqueue(beginWord);

            //loop through all words, N * N * L
            //if we use 26 lower case transformation, reduce to N * 26 * L
            // but the 26 letter transformation needs to check whether the new word is in word list or not, contains/remove all is O(N) operation
            //logic if wordList constains new word, we remove it from the list and then add to next level list, . This way we dont need the vistied array.
            // also do not add current word again to next level (queue, hashset, etc).
            // the solutions have already removed in the list/set before adding to next level, so when we loop next level, it wont be in the word list/set
            while (queue.Count > 0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    string word = queue.Dequeue();

                    //as long as we get the target, we return the value, res result level
                    if (word == endWord) return res;

                    //for (int j = 0; j < len; j++)
                    //{
                    //    if (!visited[j] && DifferByOneLetter(word, wordList[j]))
                    //    {
                    //        visited[j] = true;
                    //        queue.Enqueue(wordList[j]);
                    //    }
                    //}
                }
                res++;
            }

            return 0;

        }


        //O(L) operations, L is word length
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

        public bool DifferByOneLetterClean(string s1, string s2)
        {
            int count = 0;
            for (int i = 0; i < s1.Length; i++)
                if (s1[i] != s2[i]) count++;

            return count <= 1;
        }


        //use a-z transformation.
        public int LadderLength2(string beginWord, string endWord, IList<string> wordList)
        {
            // assumption is that beginWord is not in wordList
            // so we dont need to remove from set
            // because logic is in set, first remove, then add to queue.
            int level = 1;
            int len = beginWord.Length;
            //change list to a set, so contain, remove is O(1)
            //also remove duplicates
            HashSet<string> set = new HashSet<string>(wordList);

            //need a queue
            Queue<string> queue = new Queue<string>();

            //start the queue
            queue.Enqueue(beginWord);

            //BFS
            while(queue.Count > 0)
            {
                //level iteration, have to know which level it is
                //need a size
                int size = queue.Count;
                for(int i = 0; i < size; i++)  
                {
                    string s = queue.Dequeue();

                    //check now
                    if (s == endWord) return level;

                    //now is the trick
                    //for each letter in a string, transform it, 
                    //to char array, then replace it
                    for(int j = 0; j < len; j++) //O(L * 26)
                    {
                        //char[] c = s.ToCharArray();
                        //for (char ch = 'a'; ch <= 'z'; ch++)
                        //{
                        //    //if (ch == s[j]) continue; // skip itself, its unnecessary as we remove temp because add to queue, next level, it wont be added to queue
                        //    c[j] = ch;
                        //    string temp = new string(c);
                        //    if (set.Remove(temp))  //in set and removed
                        //    {
                        //        queue.Enqueue(temp);
                        //    }
                        //}

                        //or this way using stringbuilder
                        StringBuilder sb = new StringBuilder(s);
                        for (char ch = 'a'; ch <= 'z'; ch++)
                        {
                            sb[j] = ch;
                            if (set.Remove(sb.ToString()))  //in set and removed
                            {
                                queue.Enqueue(sb.ToString());
                            }
                        }
                    }
                }

                //go to top loop, another level
                level++;
            }

            return 0;
        }

    }
}
