using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class ProductSuggestionPrefer
    {
        class Trie
        {
            public Trie[] Children = new Trie[26];
            public List<string> Suggestion = new List<string>(); 
        }

        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            // Array.Sort(products); //O(n * logn * m);

            Trie root = new Trie();
            BuildTrie(root, products);

            List<IList<string>> result = new List<IList<string>>();

            foreach (char c in searchWord)
            {
                if(root != null) root = root.Children[c - 'a'];
                if (root != null)
                {
                    // not sorting the produdct array, but sort each suggestion array, and then take first three.
                    root.Suggestion.Sort(); // comment this line if already sorted the product array
                    result.Add(root.Suggestion.GetRange(0, Math.Min(3, root.Suggestion.Count)));
                }
                else
                {
                    result.Add(new List<string>());
                }
            }

            return result;
        }

        private void BuildTrie(Trie root, string[] products)
        {
            foreach (string word in products) // n * m 
            {
                Trie current = root;
                foreach (char c in word)
                {
                    if (current.Children[c - 'a'] == null)
                    {
                        current.Children[c - 'a'] = new Trie();
                    }
                    current = current.Children[c - 'a'];
                    current.Suggestion.Add(word);  // put products with same prefix into suggestion list.
                }
            }
        }
    }
}
