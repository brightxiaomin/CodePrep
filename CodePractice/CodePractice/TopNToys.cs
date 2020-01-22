using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class TopNToys
    {

        public  List<string> TopToys(int numToys, int topToys, string[] toys, int numQuotes, string[] quotes)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '!', '?', '\t' };
            Dictionary<string, int[]> freq = new Dictionary<string, int[]>();  // we can also use two dictionary to solve this

            foreach (string toy in toys)
            {
                freq.Add(toy.ToLower(), new int[] { 0, 0 }); // to lower may not needed, we can toggle
            }

            foreach (string quote in quotes)
            {
                HashSet<string> used = new HashSet<string>();

                string[] words = quote.ToLower().Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    if (!freq.ContainsKey(word))
                    {
                        continue;
                    }
                    int[] nums = freq[word];

                    nums[0]++;
                    if (!used.Contains(word))
                    {
                        nums[1]++;
                        used.Add(word);
                    }
                }
            }


            //rest is to find top k in N


            //C# doesnt have built in priority queue, has to implement a min-heap here 
            //linq
            string[] toysInQ = freq.Where(kv => kv.Value[0] > 0).Select(kv => kv.Key).ToArray();

            if (topToys > numToys)
                topToys = toysInQ.Length;

            // build a heap
            string[] heap = new string[topToys];

            //build min heap of size K, insert one by one, then recalculate up
            for (int i = 0; i < topToys; i++)
            {
                heap[i] = toysInQ[i];
                ReCalculateUp(heap, i, freq);
            }
            

            //loop through rest of points
            for (int j = topToys; j < toysInQ.Length; j++)
            {
                if (Compare(toysInQ[j], heap[0], freq) > 0)
                {
                    // we only care about the heap of K, rest elements we dont care
                    heap[0] = toysInQ[j];
                    ReCalculateDown(heap, 0, topToys, freq);
                }
            }


            // now heap is array contain top N toys
            // need to pop one by one
            List<string> output = new List<string>();
            for (int i = topToys - 1; i >= 0; i--)
            {
                output.Add(heap[0]);
                heap[0] = heap[i];
                ReCalculateDown(heap, 0, i + 1, freq);
            }

            //if return is array
            string[] res = new string[topToys];
            for (int i = topToys - 1; i >= 0; i--)
            {
                res[i] = heap[0];
                heap[0] = heap[i];
                ReCalculateDown(heap, 0, i + 1, freq);
            }

            //none linq way, has to use list
            //List<string> tq = new List<string>();
            //foreach (KeyValuePair<string, int[]> item in freq)
            //{
            //    if (item.Value[0] > 0)
            //        tq.Add(item.Key);
            //}


            // if topToys > total number of toys, update to the size of dictionary which has freq > 0
            //if (topToys > numToys)
            //{
            //    topToys = freq.Count(kv => kv.Value[0] > 0);

            //    // a for loop
            //    int count = 0;
            //    foreach(KeyValuePair<string, int[]> item in freq)
            //    {
            //        if (item.Value[0] > 0)
            //            ++count;
            //    }
            //}

            // build min-heap to get top frequent N toys

            //            PriorityQueue<string> pq = new PriorityQueue<>((t1, t2)-> {
            //      if (freq.get(t1)[0] != freq.get(t2)[0])
            //            {
            //                return freq.get(t1)[0] - freq.get(t2)[0];
            //            }

            //            if (freq.get(t1)[1] != freq.get(t2)[1])
            //            {
            //                return freq.get(t1)[1] - freq.get(t2)[1];
            //            }

            //            return t2.compareTo(t1);
            //        });

            //    if (topToys > numToys) {
            //      for (string toy : freq.keySet()) {
            //        if (freq.get(toy)[0] > 0) {
            //          pq.add(toy);
            //        }
            //}
            //    } else {
            //      for (string toy : toys) {
            //        pq.add(toy);

            //        if (pq.size() > topToys) {
            //          pq.poll();
            //        }
            //      }
            //    }


            //while (!pq.isEmpty()) {
            //  output.add(pq.poll());
            //}

            //Collections.reverse(output);

            return output;
        }

        public void ReCalculateDown(string[] arr, int index, int size, Dictionary<string, int[]> fre)
        {
            while (true)
            {
                int min = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;
               
                if (left < size && Compare(arr[min], arr[left], fre) > 0) min = left;
                if (right < size && Compare(arr[min], arr[right], fre) > 0) min = right;
                if (min == index) return; // NO swap

                Swap(arr, min, index);
                index = min;
            }
        }

        public void ReCalculateUp(string[] arr, int index, Dictionary<string, int[]> fre)
        {
            // has parent node and parent value > child value
            while ((index - 1) / 2 >= 0 && Compare(arr[(index - 1)/2], arr[index], fre) > 0)
            {
                Swap(arr, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public int Compare(string k1, string k2, Dictionary<string, int[]> fre)
        {
            if (fre[k1][0] != fre[k1][0])
     
                return fre[k1][0] - fre[k2][0];
    

            if (fre[k1][1] != fre[k2][1])
            
                return fre[k1][1] - fre[k2][1];
            

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
