using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    //Array based
    //least used
    // plus hashtable to store key value
    public class LRUCache
    {
        private readonly int capacity;
        private int count;
        private readonly int[] store;
        private readonly Dictionary<int, int> cache;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            count = 0;
            store = new int[capacity];
            cache = new Dictionary<int, int>();
        }

        public int Get(int key)
        {
            if (!cache.ContainsKey(key))return -1;
            //int is accessed, so it has to be moved to last position of array
            int i = 0;
            while (i < count)
            {
                if (store[i] == key) break;
                i++;
            }

            while (i < count - 1)
            {
                // move element from i + 1 to count - 1 to i > count -2
                store[i] = store[i + 1];
                i++;
            }
            store[count - 1] = key;
            return cache[key];
        }

        // most recently used, put to last position of array
        // least recently used, put to first position of array
        public void Put(int key, int value)
        {
            if(cache.ContainsKey(key))
            {
                int i = 0;
                while(i < count)
                {
                    if (store[i] == key) break;
                    i++;
                }

                while(i < count - 1 )
                {
                    // move element from i + 1 to count - 1 to i > count -2
                    store[i] = store[i + 1];
                    i++;
                }
                store[count - 1] = key;
                //update value
                cache[key] = value;
            }
            else
            {
                // if capactiy is not full, just insert
                if(count < capacity)
                    //put at last position
                    store[count++] = key;
                else
                {
                    // first delete most least used element, which is at position zero
                    // also rememer to delete this least used key/value from cache
                    cache.Remove(store[0]);
                    //move all position one level up
                    for (int i = 0; i < count - 1; i++)
                        store[i] = store[i + 1];

                    // now we have position at count - 1
                    store[count - 1] = key;
                }
                cache.Add(key, value);
            }
        }
    }
}
