using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class TopNToysClean
    {
        public List<string> TopToys(int numToys, int topToys, string[] toys, int numQuotes, string[] quotes)
        {
            List<string> output = new List<string>();

            if (numToys == 0 || topToys == 0 || numQuotes == 0 || toys == null || quotes == null)
                return output;

            char[] delimiterChars = { ' ', ',', '.', ':', '!', '?', '\t' };

            Dictionary<string, int[]> freq = new Dictionary<string, int[]>();  
            foreach (string toy in toys)
            {
                freq.Add(toy.ToLower(), new int[] { 0, 0 }); 
            }

            foreach (string quote in quotes)
            {
                HashSet<string> used = new HashSet<string>();

                string[] words = quote.ToLower().Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    if (!freq.ContainsKey(word)) continue;

                    int[] nums = freq[word];
                    nums[0]++;
                    if (!used.Contains(word))
                    {
                        nums[1]++;
                        used.Add(word);
                    }
                }
            }

            //C# doesn't have built in priority queue, has to implement a min-heap below 

            string[] toysInQuote = freq.Where(kv => kv.Value[0] > 0).Select(kv => kv.Key).ToArray();

            if (topToys > toysInQuote.Length)
                topToys = toysInQuote.Length;

            // array to store min heap of size topToys
            string[] heap = new string[topToys];
            //build min heap, insert one by one, then recalculate up
            for (int i = 0; i < topToys; i++)
            {
                heap[i] = toysInQuote[i];
                ReCalculateUp(heap, i, freq);
            }

            //loop through rest of toys in quote
            for (int i = topToys; i < toysInQuote.Length; i++)
            {
                if (Compare(toysInQuote[i], heap[0], freq) > 0)
                {
                    heap[0] = toysInQuote[i];
                    ReCalculateDown(heap, 0, topToys, freq);
                }
            }

            // now heap is array contain top toys
            for (int i = topToys - 1; i >= 0; i--)
            {
                output.Add(heap[0]);
                heap[0] = heap[i];
                ReCalculateDown(heap, 0, i + 1, freq);
            }

            output.Reverse();

            return output;
        }

        public void ReCalculateDown(string[] arr, int index, int size, Dictionary<string, int[]> freq)
        {
            while (true)
            {
                int min = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;

                if (left < size && Compare(arr[min], arr[left], freq) > 0) min = left;
                if (right < size && Compare(arr[min], arr[right], freq) > 0) min = right;
                if (min == index) return; // NO swap

                Swap(arr, min, index);
                index = min;
            }
        }

        public void ReCalculateUp(string[] arr, int index, Dictionary<string, int[]> freq)
        {
            // has parent node and parent value > child value
            while (index > 0 && Compare(arr[(index - 1) / 2], arr[index], freq) > 0)
            {
                Swap(arr, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public int Compare(string k1, string k2, Dictionary<string, int[]> freq)
        {
            if (freq[k1][0] != freq[k2][0])
                return freq[k1][0] - freq[k2][0];

            if (freq[k1][1] != freq[k2][1])
                return freq[k1][1] - freq[k2][1];

            return k2.CompareTo(k1);
        }

        public void Swap(string[] arr, int a, int b)
        {
            string temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
