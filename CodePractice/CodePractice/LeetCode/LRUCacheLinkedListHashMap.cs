using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    //O(1) operations for GET, PUT
    public class LRUCacheLinkedListHashMap
    {
        private readonly Dictionary<int, DLinkedNode> cache = new Dictionary<int, DLinkedNode>();
        private int size;
        private readonly int capacity;
        private readonly DLinkedNode head, tail;

        public LRUCacheLinkedListHashMap(int capacity)
        {
            this.size = 0;
            this.capacity = capacity;

            //Dummy Node, always there
            head = new DLinkedNode();
            tail = new DLinkedNode();

            head.Next = tail;
            tail.Prev = head;
        }


        private void AddNode(DLinkedNode node)
        {
            /**
             * Always add the new node right after head.
             */
            node.Prev = head;
            node.Next = head.Next;

            head.Next.Prev = node;
            head.Next = node;
        }

        private void RemoveNode(DLinkedNode node)
        {
            /**
             * Remove an existing node from the linked list.
             */
            DLinkedNode Prev = node.Prev;
            DLinkedNode Next = node.Next;

            Prev.Next = Next;
            Next.Prev = Prev;
        }

        private void MoveToHead(DLinkedNode node)
        {
            /**
             * Move certain node in between to the head.
             */
            RemoveNode(node);
            AddNode(node);
        }

        private DLinkedNode PopTail()
        {
            /**
             * Pop the current tail.
             */
            DLinkedNode res = tail.Prev;
            RemoveNode(res);
            return res;
        }

        public int Get(int key)
        {
            if (!cache.ContainsKey(key)) return -1;

            DLinkedNode node = cache[key];
            // move the accessed node to the head;
            MoveToHead(node);

            return node.Value;
        }

        public void Put(int key, int value)
        {
           
            if (!cache.ContainsKey(key))
            {
                DLinkedNode newNode = new DLinkedNode
                {
                    Key = key,
                    Value = value
                };

                cache.Add(key, newNode);
                AddNode(newNode);
                ++size;

                if (size > capacity)
                {
                    // pop the tail
                    DLinkedNode tail = PopTail();
                    cache.Remove(tail.Key);
                    --size;
                }
            }
            else
            {
                // update the value.
                DLinkedNode node = cache[key];
                node.Value = value;
                MoveToHead(node);
            }
        }

        class DLinkedNode
        {
            public int Key { get; set; }
            public int Value { get; set; }
            public DLinkedNode Prev { get; set; }
            public DLinkedNode Next { get; set; }
        }
    }
}
