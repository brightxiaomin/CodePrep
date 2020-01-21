using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    public class KClosestPoints
    {
        public int[][] KClosest(int[][] points, int K)
        {
            if (points == null || points.Length == 0)
                return points;

            if (K > points.Length)
                K = points.Length;

            int[][] res = new int[K][];

            //build max heap of size K, insert one by one, then recalculate up
            for(int i = 0; i < K; i++)
            {
                res[i] = points[i];
                RecalculateUp(res, i);
            }

            //loop through rest of points
            for (int j = K; j < points.Length; j++)
            {
                if (Dist(points[j]) < Dist(res[0]))
                {
                    // we only care about the heap of K, rest elements we dont care
                    res[0] = points[j];
                    Heapify(res, 0, K);
                }
            }

            return res;
        }

        public void Heapify(int[][] res, int index, int size)
        {
            while (true)
            {
                int max = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                if (left < size && Dist(res[max]) < Dist(res[left])) max = left;
                if (right < size && Dist(res[max]) < Dist(res[right])) max = right;
                if (max == index) return; // NO swap

                Swap(res, max, index);
                index = max;
            }
        }

        public void RecalculateUp(int[][] res, int index)
        {
            // has parent node and parent value < child value
            while ((index - 1)/2 >= 0 && Dist(res[(index - 1)/2]) < Dist(res[index])) 
            {
                Swap(res, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public int Dist(int[] point)
        {
            return point[0] * point[0] + point[1] * point[1];
        }

        public void Swap(int[][] points, int a, int b)
        {
            int[] temp = points[a];
            points[a] = points[b];
            points[b] = temp;
        }

    }

}
