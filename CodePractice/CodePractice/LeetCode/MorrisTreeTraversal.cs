using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.LeetCode
{
    class MorrisTreeTraversal
    {
        class Node
        {
            public int data;
            public Node left_node, right_node;

            Node(int item)
            {
                data = item;
                left_node = null;
                right_node = null;
            }
        }

        
        class Tree
        {
            Node root;

            void MorrisInOrder(Node root)
            {
                Node curr, prev;

                if (root == null)
                    return;

                curr = root;
                while (curr != null)
                {
                    if (curr.left_node == null)
                    {
                        Console.WriteLine(curr.data + " ");
                        curr = curr.right_node;
                    }
                    else
                    {
                        /* Find the previous (prev) of curr */
                        prev = curr.left_node;
                        while (prev.right_node != null && prev.right_node != curr)
                            prev = prev.right_node;

                        /* Make curr as right child of its prev */
                        if (prev.right_node == null)
                        {
                            prev.right_node = curr;
                            curr = curr.left_node;
                        }

                        /* fix the right child of prev*/

                        else
                        {
                            prev.right_node = null;
                            Console.WriteLine(curr.data + " "); // Visit here, come back to current node, left subtree has be traversed already, now come back
                            curr = curr.right_node;
                        }

                    }

                }
            }

            //public List<Integer> preorderTraversal(TreeNode root)
            //{
            //    LinkedList<Integer> output = new LinkedList<>();

            //    TreeNode node = root;
            //    while (node != null)
            //    {
            //        if (node.left == null)
            //        {
            //            output.add(node.val); // Visit
            //            node = node.right;
            //        }
            //        else
            //        {
            //            TreeNode predecessor = node.left;
            //            while ((predecessor.right != null) && (predecessor.right != node))
            //            {
            //                predecessor = predecessor.right;
            //            }

            //            if (predecessor.right == null)
            //            {
            //                output.add(node.val);  // Visit before even processing the left 
            //                predecessor.right = node;
            //                node = node.left;
            //            }
            //            else
            //            {
            //                predecessor.right = null; // come back to current
            //                node = node.right;
            //            }
            //        }
            //    }
            //}


        }
    }
}
