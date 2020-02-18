using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    public class SerializeBinaryTree
    {

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            //BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                if(node == null)
                {
                    sb.Append("#,");
                    continue;
                }
                //else
                //{
                    sb.Append(string.Format("{0}{1}", node.val, ","));
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                //}
            }

            return sb.ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            int len = data.Length - 1;
            data.Remove(len);
            string[] val = data.Split(new char[] { ',' });
            if (val[0] == "#") return null;
            //bool[] visited = new bool[val.Length];
            TreeNode root = new TreeNode(int.Parse(val[0]));
            //TreeNode current = root;
            //visited[0] = true;

            //for(int i = 0; i < val.Length; i++)
            //{
            //    if (visited[i]) continue;

            //        int left = 2 * i + 1;
            //    int right = 2 * i + 2;

            //    if (left < val.Length && val[left] != "#")
            //        current.left = new TreeNode(int.Parse(val[left]));
            //}

            // again use BFS, cause you used to serialize
            int i = 1;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while(queue.Count > 0 && i < data.Length)
            {
                TreeNode current = queue.Dequeue();
                if(val[i] != "#")
                {
                    TreeNode left = new TreeNode(int.Parse(val[i]));
                    current.left = left;
                    queue.Enqueue(left);
                }
                i++;
                if (val[i] != "#")
                {
                    TreeNode right = new TreeNode(int.Parse(val[i]));
                    current.right = right;
                    queue.Enqueue(right);
                }
                i++;
            }

            return root;
        }


        public string serialize2(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            //BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                if (node == null)
                {
                    sb.Append("#,");
                    continue;
                }
                sb.Append(string.Format("{0}{1}", node.val, ","));
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }

            string res = sb.ToString();
            return res.Remove(res.Length - 1);
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize2(string data)
        {
            string[] val = data.Split(new char[] { ',' });
            if (val[0] == "#") return null;
            TreeNode root = new TreeNode(int.Parse(val[0]));
            int i = 1;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0 && i < data.Length)
            {
                TreeNode current = queue.Dequeue();
                if (val[i] != "#")
                {
                    TreeNode left = new TreeNode(int.Parse(val[i]));
                    current.left = left;
                    queue.Enqueue(left);
                }
                i++;
                if (val[i] != "#")
                {
                    TreeNode right = new TreeNode(int.Parse(val[i]));
                    current.right = right;
                    queue.Enqueue(right);
                }
                i++;
            }

            //for loop is also fine, but still queue is neeeded
            //for (int i = 1; i < val.Length; i++)
            //{
            //    TreeNode parent = queue.Dequeue();
            //    if (!val[i].Equals("#"))
            //    {
            //        TreeNode left = new TreeNode(int.Parse(val[i]));
            //        parent.left = left;
            //        queue.Enqueue(left);
            //    }
            //    if (!val[++i].Equals("n#"))
            //    {
            //        TreeNode right = new TreeNode(int.Parse(val[i]));
            //        parent.right = right;
            //        queue.Enqueue(right);
            //    }
            //}

            return root;
        }

        public string serialize3(TreeNode root)
        {
            return serial(new StringBuilder(), root).ToString();
        }

        // Generate preorder string
        private StringBuilder serial(StringBuilder str, TreeNode root)
        {
            if (root == null) return str.Append("#,");
            str.Append(root.val).Append(",");
            serial(str, root.left);
            serial(str, root.right);
            return str;
        }

        public TreeNode deserialize3(String data)
        {
            return deserial(new LinkedList<string>(data.Split(new char[] { ',' })));
        }

        // Use queue to simplify position move
        private TreeNode deserial(LinkedList<string> q)
        {
            string val = q.First();
            q.RemoveFirst();
            if (val == "#") return null;
            TreeNode root = new TreeNode(int.Parse(val));
            root.left = deserial(q);
            root.right = deserial(q);
            return root;
        }
    }
}
