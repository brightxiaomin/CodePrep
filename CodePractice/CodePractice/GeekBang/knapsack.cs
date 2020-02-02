using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    class Knapsack
    {

        // backtrack way, all input has been defined as class members
        private int maxW = int.MinValue; // the result for max weight
        private int maxValue = int.MinValue; // result for max value
        private readonly int[] weight = { 2, 4, 7, 9, 15 };
        private int[] value = { 3, 4, 8, 9, 6 }; // value for item
        private readonly int num = 5; // item numbers
        private readonly int capacity = 12; // capacity

        public void CalculateMax(int i, int cw)  // starting point, call f(0,0)
        { 
            //cw = current weight
            if (cw == capacity || i == num) 
            { // cw == w means bag is full, i == n mean all items have been processed
                if (cw > maxW) maxW = cw;
                return;
            }
            CalculateMax(i + 1, cw); // not pick item i
            if (cw + weight[i] <= capacity)
            {
                CalculateMax(i + 1, cw + weight[i]); // select item i
            }

        }

        // max value version
        // backtracking, cannot use memo
        public void CalculateMaxValue(int i, int cw, int cv)
        {
            //cw = current weight
            //cv = current value
            if (cw == capacity || i == num) // constraint is capacity and num
            { // cw == w means bag is full, i == n mean all items have been processed
                if (cv > maxValue) maxValue = cv;
                return;
            }
            CalculateMaxValue(i + 1, cw, cv); // not pick item i
            if (cw + weight[i] <= capacity)
            {
                CalculateMaxValue(i + 1, cw + weight[i], cv + value[i]); // select item i
            }
        }

        //improve way of backtracking with memoization
        public void CalculateMaxUsingMemo(int i, int cw)
        {
            //array to store previous calculated results
            // using bool typed to store whether it has been reached or not
            bool[,] memo = new bool[num, capacity + 1];

            // same logic applies
            if (cw == capacity || i == num)
            {
                if (cw > maxW) maxW = cw;
                return;
            }

            //current stack is f(i,cw), so if it is reached, we just return
            // this way the first time f(i,cw) is reached will be calculated all the way down
            if (memo[i, cw]) return;

            //to this step, means not reached, updated memo
            memo[i, cw] = true;

            // fist time for f(i, cw) stil need to continue calculation
            CalculateMaxUsingMemo(i + 1, cw); // not pick item i
            if (cw + weight[i] <= capacity)
            {
                CalculateMaxUsingMemo(i + 1, cw + weight[i]); // select item i
            }
        }


        public int GetMax()
        {
            return maxW;
        }


        //DP Approach using sentinel
        //using sentinel, row has more than one,
        // real index for weight array is i - 1
        public int GetMaxDP()
        {
            //store states at each stage
            bool[][] states = new bool[num + 1][];
            for(int i = 0; i < num + 1; i++)
            {
                states[i] = new bool[capacity + 1];
            }

            //0,0 position is sentinel, assign to true
            states[0][0] = true;

            //state transition from one stage to next stage
            // real stage is 1 - n for n items
            for(int i = 1; i <=num; i++)
            {
                for(int j = 0; j <=capacity; j++)
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
                for(int j = 0; j <= capacity - weight[i - 1]; j++)
                {
                    if (states[i - 1][j]) states[i][j + weight[i - 1]] = true;
                }
            }

            for (int i = capacity; i >= 0; i--)
                // we need find largest index which is true (reached) in the last stage
                if (states[num][i]) return i; // index i is weight

            return 0;
        }

        //optimize to save some space, one d array
        public int GetMaxDP2()
        {
            bool[] states = new bool[capacity + 1]; //default value all false
            states[0] = true;  // can be optimized using sentinel
            if (weight[0] <= capacity)
            {
                states[weight[0]] = true; // first item processed here
            }
            for (int i = 1; i < num; ++i)
            {
                // remember to process in reverse order,otherwise, wrong result, because if we process from small to big
                // we may change last current state at certain index, which may be used by later processing
                // if mean we need a value of last state at a index later, but we assign it to a different value before we actually use it
                // from big to small avoid this problem, we only assign values after biggest j, but the state we need is all state[j] before biggest j.
                // while small to big has issue, we need state[j] from 0 to j, when we process from 0 to j, we may change the state from 0 to j
                // they need to be kept untouched before this round of processing.
                for (int j = capacity - weight[i]; j >= 0; --j) 
                {//pick item i
                    if (states[j] == true) states[j + weight[i]] = true;
                }
            }
            for (int i = capacity; i >= 0; --i)
            { // output
                if (states[i] == true) return i; // index is weight
            }
            return 0;
        }


        //calcualte max value using DP
        //using sentinel, row has more than one,
        // real index for weight array and value array is i - 1
        public int GetMaxValueDP()
        {
            //store states at each stage
            // change from bool to int, to not only store vistied or not (value > 0), but also store max value at certain weight
            //first row is sentinel row
            int [][] states = new int [num + 1][];
            states[0] = Enumerable.Repeat(0, capacity + 1).ToArray();  //sentinel row has to been all visited, but value is 0, as no items in it

            for (int i = 1; i < num + 1; i++)
            {
                states[i] = Enumerable.Repeat(-1, capacity + 1).ToArray();
            }



            //state transition from one stage to next stage
            // real stage is 1 - n for n items
            for (int i = 1; i <= num; i++)
            {
                for (int j = 0; j <= capacity; j++)
                {
                    //not put item i
                    if (states[i - 1][j] >= 0) states[i][j] = states[i - 1][j];
                }

                //second loop to put item i
                // recond to minus 1 for the weight index
                // need store max value at spot of states
                for (int j = 0; j <= capacity - weight[i - 1]; j++)
                {
                    if (states[i - 1][j] >= 0)
                    {
                        states[i][j + weight[i - 1]] = Math.Max(states[i][j + weight[i - 1]], states[i - 1][j] + value[i-1]);
                    }
                }
            }

            int maxValue = -1;
            for (int i = capacity; i >= 0; i--)  // noraml order or reverse order are both fine, we just need get max at last row
                if (states[num][i] > maxValue) maxValue = states[num][i]; 

            return maxValue;
        }

        public int GetMaxValueDP2()
        {
            int[] states = Enumerable.Repeat(-1, capacity + 1).ToArray(); //default value all false
            
            states[0] = 0;  
            if (weight[0] <= capacity)
            {
                states[weight[0]] = value[0]; // first item processed here
            }

            for (int i = 1; i < num; ++i) // 2 to num items, index 1 to num -1
            {
                for (int j = capacity - weight[i]; j >= 0; --j)
                {
                    //pick item i
                    // previous max at this position
                    if (states[j] >= 0) states[j + weight[i]] = Math.Max(states[j + weight[i]], states[j] + value[i - 1]);
                }
            }

            int maxValue = -1;
            for (int i = capacity; i >= 0; --i) // normal order or reverse order are both fine, we just need max index range (0, capacity)
            { // output
                if (states[i] > maxValue) maxValue = states[i];
            }
            return maxValue;
        }

    }
}
