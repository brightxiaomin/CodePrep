using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class ProductSuggetion
    {
        public List<List<string>> GetSuggestedProducts(string[] products, string searchWord)
        {
            Trie root = new Trie();
            foreach (string p in products)
            { // build Trie.
                Trie t = root;
                foreach (char c in p)
                { // insert current product into Trie.
                    if (t.Sub[c - 'a'] == null)
                        t.Sub[c - 'a'] = new Trie();
                    t = t.Sub[c - 'a'];
                    t.Suggestion.Add(p); // put products with same prefix into suggestion list.
                    t.Suggestion.Sort(); // sort products in the suggestions
                    if (t.Suggestion.Count > 3) // maintain 3 lexicographically minimum strings.
                    {
                        int lastIndex = t.Suggestion.Count - 1;
                        t.Suggestion.RemoveAt(lastIndex);
                    }
                        
                }
            }
            List<List<string>> ans = new List<List<string>>();
            foreach (char c in searchWord)
            { // search product.
                if (root != null) // if current Trie is NOT null.
                    root = root.Sub[c - 'a'];
                ans.Add(root == null ? new List<string>() : root.Suggestion); // add it if there exist products with current prefix.
            }
            return ans;
        }


        //Brutal Force, what surprised me is 
        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            Array.Sort(products, StringComparer.Ordinal);
            IList<IList<string>> result = new List<IList<string>>();
            string keyword = "";
            for (int i = 0; i < searchWord.Length; i++)
            {
                keyword += searchWord[i];
                IList<string> current = new List<string>();
                int count = 0;
                foreach (string product in products)
                {
                    if (product.StartsWith(keyword, StringComparison.Ordinal))
                    {
                        current.Add(product);
                        count++;
                    }
                    if (count == 3)
                    {
                        break;
                    }
                }
                result.Add(current);
            }
            return result;
        }
    }

    class Trie
    {
       public Trie[] Sub = new Trie[26];
        public List<string> Suggestion = new List<string>();
    }
}


/*
 * Analysis:
    Complexity depends on the process of building Trie and the length of searchWord. For each Trie Node, sorting suggestion List involving comparing String, hence cost time O(m), but space cost only O(1) due to suggestion List save only String referrence. Therefore,
    Time: O(m * m * n + L), space: O(m * n + L), where m = average length of products, n = products.length, L = searchWord.length().
 * 
 */
