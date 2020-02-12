using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class LFUCache
    {
        private readonly int capacity;
        private int curSize;
        private int minFrequency;
        private readonly Dictionary<int, DLLNode> cache;
        private readonly Dictionary<int, DoubleLinkedList> frequencyMap;

        /*.*/
        /*
        * @param capacity: total capacity of LFU Cache
        * @param curSize: current size of LFU cache
        * @param minFrequency: frequency of the last linked list (the minimum frequency of entire LFU cache)
        * @param cache: a hash map that has key to Node mapping, which used for storing all nodes by their keys
        * @param frequencyMap: a hash map that has key to linked list mapping, which used for storing all
        * double linked list by their frequencies
        * */
        public LFUCache(int capacity)
        {
            this.capacity = capacity;
            this.curSize = 0;
            this.minFrequency = 0;

            this.cache = new Dictionary<int, DLLNode>();
            this.frequencyMap = new Dictionary<int, DoubleLinkedList>();
        }

        /** get node value by key, and then update node frequency as well as relocate that node **/
        public int Get(int key)
        {
            if (!cache.ContainsKey(key)) return -1;
            DLLNode curNode = cache[key];
            UpdateNode(curNode);
            return curNode.Value;
        }

        /**
         * add new node into LFU cache, as well as double linked list
         * condition 1: if LFU cache has input key, update node value and node position in list
         * condition 2: if LFU cache does NOT have input key
         *  - sub condition 1: if LFU cache does NOT have enough space, remove the Least Recent Used node
         *  in minimum frequency list, then add new node
         *  - sub condition 2: if LFU cache has enough space, add new node directly
         * **/
        public void Put(int key, int value)
        {
            // corner case: check cache capacity initialization
            if (capacity == 0)
            {
                return;
            }

            if (cache.ContainsKey(key))
            {
                DLLNode curNode = cache[key];
                curNode.Value = value;
                UpdateNode(curNode);
            }
            else
            {
                curSize++;
                if (curSize > capacity)
                {
                    // get minimum frequency list
                    DoubleLinkedList minFreqList = frequencyMap[minFrequency];
                    DLLNode deleteNode = minFreqList.RemoveTail();
                    cache.Remove(deleteNode.Key);
                    curSize--;
                }
                // reset min frequency to 1 because of adding new node
                minFrequency = 1;
                DLLNode newNode = new DLLNode(key, value);

                // get the list with frequency 1, and then add new node into the list, as well as into LFU cache
                DoubleLinkedList curList = frequencyMap.ContainsKey(1) ? frequencyMap[1] : new DoubleLinkedList();
                curList.AddNode(newNode);
                 // do not add duplicate
                if (frequencyMap.ContainsKey(1))
                    frequencyMap[1] = curList;
                else
                    frequencyMap.Add(1, curList);
                cache.Add(key, newNode);
            }
        }

        public void UpdateNode(DLLNode curNode)
        {
            int curFreq = curNode.Frequency;
            DoubleLinkedList curList = frequencyMap[curFreq];
            curList.RemoveNode(curNode);

            // if current list the the last list which has lowest frequency and current node is the only node in that list
            // we need to remove the entire list and then increase min frequency value by 1
            if (curFreq == minFrequency && curList.listSize == 0)
            {
                minFrequency++;
            }

            curNode.Frequency++;
            // add current node to another list has current frequency + 1,
            // if we do not have the list with this frequency, initialize it
            DoubleLinkedList newList = frequencyMap.ContainsKey(curNode.Frequency) ? frequencyMap[curNode.Frequency] : new DoubleLinkedList();
            newList.AddNode(curNode);
            // do not add duplicate
            if (frequencyMap.ContainsKey(curNode.Frequency))
                frequencyMap[curNode.Frequency] = newList;
            else
                frequencyMap.Add(curNode.Frequency, newList);
        }

        /*
        * @param key: node key
        * @param val: node value
        * @param frequency: frequency count of current node
        * (all nodes connected in same double linked list has same frequency)
        * @param prev: previous pointer of current node
        * @param next: next pointer of current node
        * */
        public class DLLNode
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public int Frequency { get; set; }
            public DLLNode Prev { get; set; }
            public DLLNode Next { get; set; }


            public DLLNode(int key, int val)
            {
                Key = key;
                Value = val;
                Frequency = 1;
            }
        }

        /*
        * @param listSize: current size of double linked list
        * @param head: head node of double linked list
        * @param tail: tail node of double linked list
        * */
        public class DoubleLinkedList
        {
            public int listSize;
            DLLNode head;
            DLLNode tail;
            public DoubleLinkedList()
            {
                this.listSize = 0;
                this.head = new DLLNode(0, 0);
                this.tail = new DLLNode(0, 0);
                head.Next = tail;
                tail.Prev = head;
            }

            /** add new node into head of list and increase list size by 1 **/
            public void AddNode(DLLNode curNode)
            {
                DLLNode nextNode = head.Next;
                curNode.Next = nextNode;
                curNode.Prev = head;
                head.Next = curNode;
                nextNode.Prev = curNode;
                listSize++;
            }

            /** remove input node and decrease list size by 1**/
            public void RemoveNode(DLLNode curNode)
            {
                DLLNode prevNode = curNode.Prev;
                DLLNode nextNode = curNode.Next;
                prevNode.Next = nextNode;
                nextNode.Prev = prevNode;
                listSize--;
            }

            /** remove tail node **/
            public DLLNode RemoveTail()
            {
                // DO NOT FORGET to check list size
                if (listSize > 0)
                {
                    DLLNode tailNode = tail.Prev;
                    RemoveNode(tailNode);
                    return tailNode;
                }
                return null;
            }
        }
    }
}
