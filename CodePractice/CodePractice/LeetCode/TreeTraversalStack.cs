using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    class TreeTraversalStack
    {
        //public List<Integer> preorderTraversal(TreeNode root)
        //{
        //    LinkedList<TreeNode> stack = new LinkedList<>();
        //    LinkedList<Integer> output = new LinkedList<>();
        //    if (root == null)
        //    {
        //        return output;
        //    }

        //    stack.add(root);
        //    while (!stack.isEmpty())
        //    {
        //        //out put before process, inside the stack
        //        // before process any left or right 
        //        TreeNode node = stack.pollLast();
        //        output.add(node.val);
        //        if (node.right != null)
        //        {
        //            stack.add(node.right);
        //        }
        //        if (node.left != null)
        //        {
        //            stack.add(node.left);
        //        }
        //    }
        //    return output;
        //}

        //Adopt this one for stack imp for in order,
        // first go all the way left, output, then one step to the left
        //public List<Integer> inorderTraversal(TreeNode root)
        //{
        //    List<Integer> res = new ArrayList<>();
        //    Stack<TreeNode> stack = new Stack<>();
        //    TreeNode curr = root;
        //    while (curr != null || !stack.isEmpty())
        //    {
        //        while (curr != null)
        //        {
        //            stack.push(curr);
        //            curr = curr.left;
        //        }
        //        // left has been processed then we out put
        //        curr = stack.pop();
        //        res.add(curr.val);
        //        curr = curr.right;
        //    }
        //    return res;
        //}

        //use linked list
        //this is actually a right PREORDER (parent node -> right child ->left child ) and then reverse it.
        public List<int> PostorderTraversal(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            LinkedList<int> output = new LinkedList<int>();
            if (root == null)
            {
                return output.ToList();
            }
            
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                output.AddFirst(node.val);
                if (node.left != null)
                {
                    stack.Push(node.left);
                }
                if (node.right != null)
                {
                    stack.Push(node.right);
                }
            }
            return output.ToList();
        }

        //use list
        public List<int> PostorderTraversal2(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            List<int> output = new List<int>();
            if (root == null)
            {
                return output;
            }

            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                output.Add(node.val);
                if (node.left != null)
                {
                    stack.Push(node.left);
                }
                if (node.right != null)
                {
                    stack.Push(node.right);
                }
            }
            output.Reverse();
            return output;
        }

        // https://leetcode.com/problems/binary-tree-postorder-traversal/discuss/45582/A-real-Postorder-Traversal-.without-reverse-or-insert-4ms
        //real post order using double pushing
        public List<int> PostorderTraversal3(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            List<int> output = new List<int>();
            if (root == null)
            {
                return output;
            }

            stack.Push(root);
            stack.Push(root);
            TreeNode cur;
            while (stack.Count > 0)
            {
                cur = stack.Peek();
                stack.Pop();
                if (stack.Count > 0 && stack.Peek() == cur)
                {
                    if (cur.right != null)
                    {
                        stack.Push(cur.right);
                        stack.Push(cur.right);
                    }
                    if (cur.left != null)
                    {
                        stack.Push(cur.left);
                        stack.Push(cur.left);
                    }
                }
                else
                    output.Add(cur.val);
            }
            return output;

        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }
    }
}
