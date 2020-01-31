using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Knapsack
    {

        // 回溯算法实现。注意：我把输入的变量都定义成了成员变量。
        private int maxW = int.MinValue; // 结果放到maxW中
        private int[] weight = { 2, 4, 7, 9, 15 };  // 物品重量
        private int n = 5; // 物品个数
        private int w = 12; // 背包承受的最大重量
        public void f(int i, int cw)
        { // 调用f(0, 0)
            if (cw == w || i == n)
            { // cw==w表示装满了，i==n表示物品都考察完了
                if (cw > maxW) maxW = cw;
                return;
            }
            f(i + 1, cw); // 选择不装第i个物品
            if (cw + weight[i] <= w)
            {
                f(i + 1, cw + weight[i]); // 选择装第i个物品
            }

        }


        public int GetMax()
        {
            return maxW;
        }


        //DP Approach using sentinel
        public int GetMaxDP()
        {
            //store states at each stage
            bool[][] states = new bool[n + 1][];
            for(int i = 0; i < n + 1; i++)
            {
                states[i] = new bool[w + 1];
            }

            //0,0 position is sentinel, assign to true
            states[0][0] = true;

            //state transition from one stage to next stage
            // real stage is 1 - n for n items
            for(int i = 1; i <=n; i++)
            {
                for(int j = 0; j <=w; j++)
                {
                    //only if i-1, j has been reached
                    // can i decide whether to put item i on top of j or not
                    //i - 1 reached j, now what, two directions.

                    //not put item i
                    if (states[i - 1][j]) states[i][j] = states[i - 1][j];

                    // in one for loop
                    //if (states[i - 1][j] && j <= w - weight[i - 1]) states[i][j + weight[i - 1]] = true;
                }

                //second loop to put item i
                // recond to minus 1 for the weight index
                for(int j = 0; j <= w - weight[i - 1]; j++)
                {
                    if (states[i - 1][j]) states[i][j + weight[i - 1]] = true;
                }
            }

            for (int i = w; i >= 0; i--)
                // we need find largest index which is true (reached) in the last stage
                if (states[n][i]) return i;

            return 0;
        }

        //optimize to save some space, one d array

        public int GetMaxDP2(int[] items, int n, int w)
        {
            bool[] states = new bool[w + 1]; // 默认值false
            states[0] = true;  // 第一行的数据要特殊处理，可以利用哨兵优化
            if (items[0] <= w)
            {
                states[items[0]] = true;
            }
            for (int i = 1; i < n; ++i)
            { // 动态规划
                for (int j = w - items[i]; j >= 0; --j)
                {//把第i个物品放入背包
                    if (states[j] == true) states[j + items[i]] = true;
                }
            }
            for (int i = w; i >= 0; --i)
            { // 输出结果
                if (states[i] == true) return i;
            }
            return 0;
        }

    }
}
