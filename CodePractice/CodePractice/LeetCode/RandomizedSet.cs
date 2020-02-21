using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{

    // this is key part
    //delete part is to swap element with last one 
    // then remove last one
    public class RandomizedSet
    {
        private readonly Dictionary<int, int> store; // key is value, value is index
        private readonly List<int> set;
        private readonly Random random;
        /** Initialize your data structure here. */
        public RandomizedSet()
        {
            store = new Dictionary<int, int>();
            set = new List<int>();
            random = new Random();
        }

        /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
        public bool Insert(int val)
        {
            if (store.ContainsKey(val))  //search O(1)
                return false;

            int size = set.Count;  // index for the to be inserted item

            store.Add(val, size); // dict add, o1
            set.Add(val); // list, add O1
            return true;

        }

        /** Removes a value from the set. Returns true if the set contained the specified element. */
        public bool Remove(int val)
        {
            if (!store.ContainsKey(val))  //search O(1)
                return false;

            int index = store[val];   // get index from val in O(1), thats why need dict
            int lastIndex = set.Count - 1;

            //Key part
            //swap, put last element to the index
            int lastElement = set[lastIndex]; 
            set[index] = lastElement;

            //update index for the last element value because it is swaped, now the list is valid, all spot has values
            //we know all element in list is always in the dict because no duplicate
            store[lastElement] = index;

            //now we dont care about last element since it is stored at index position
            set.RemoveAt(lastIndex);
            //remove current val
            store.Remove(val);

            return true;
        }

        /** Get a random element from the set. */
        public int GetRandom()
        {
            int index = random.Next(set.Count);
            return set[index];

        }

        
    }
}
