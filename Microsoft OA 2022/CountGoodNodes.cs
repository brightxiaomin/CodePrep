using System;
using System.Collections.Generic;
using System.Text;

namespace MicrosoftOA
{
    class CountGoodNodes
    {
        public int GoodNodes(TreeNode root)
        {
            traverse(root, root.val);
            return count;

        }

        int count = 0;

        // 二叉树遍历函数，pathMax 参数记录从根节点到当前节点路径中的最大值
        void traverse(TreeNode root, int pathMax)
        {
            if (root == null)
            {
                return;
            }
            if (pathMax <= root.val)
            {
                // 找到一个「好节点」
                count++;
            }
            // 更新路径上的最大值
            pathMax = Math.Max(pathMax, root.val);

            traverse(root.left, pathMax);
            traverse(root.right, pathMax);
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
