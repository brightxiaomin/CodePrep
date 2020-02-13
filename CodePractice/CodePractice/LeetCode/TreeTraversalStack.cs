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
    }
}
