public int[][] kClosest(int[][] points, int K) {
        if (points == null || points.length == 0)
            return points;
        if (K > points.length)
            K = points.length;
        int[][] res = new int[K][2];
        for(int i = 0; i < K; i++)
        {
            res[i] = points[i];
            swim(res, i, K);
        }
        for (int i = K; i < points.length; i++)
        {
            if (dist(res[0]) > dist(points[i]))
            {
                res[0] = points[i];
                sink(res, 0, K);
            }
        }
        return res;
        
    }
    private void sink(int[][] res, int n, int K)
    {
        int dist = dist(res[n]);
        while (2*n+1 < K)
        {
            int kid = n*2+1; //left child
            int dist1 = dist(res[kid]); //right child if exist
            int dist2 = Integer.MIN_VALUE;
            if (kid + 1 < K)
            	dist2 = dist(res[kid+1]);
            if(dist1 < dist2)
                kid++;
            if (dist > Math.max(dist1, dist2))
                break;
            swap(res, n, kid);           
            n = kid;
        }
    }
    private void swim(int[][] res, int n, int K)
    {
        int dist = dist(res[n]);
        while (n > 0)
        {
            int d = dist(res[(n-1)/2]); //parent
            if (d < dist)
            {
                swap(res, n, (n-1)/2);
                n = (n-1)/2;
            }
            else
                break;
        }
    }
    private void swap(int[][] res, int a, int b)
    {
        int[] t = res[a];
        res[a] = res[b];
        res[b] = t;
    }
    
    private int dist(int[] point)
    {
    	return point[0]*point[0] + point[1]*point[1];
    }