using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class DecodeString
    {
        //decode string
        public static string DecodeStringWay(string s, int rows)
        {
            // assume s.length is divided by n
            int cols = s.Length / rows;
            char[,] mat = new char[rows, cols];
            int pos = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mat[i, j] = s[pos++];
                }
            }
            //All diagonal elements have same j-i difference
            //string result = "";
            StringBuilder sb = new StringBuilder();
            for (int diag = 0; diag < cols; diag++)
            {
                //j-i=diag. So j=diag+i
                for (int i = 0; i < rows; i++)
                {
                    if (diag + i >= cols) break;
                    if (mat[i, i + diag] == '_') sb.Append(" ");
                    else sb.Append(mat[i, i + diag]);
                }
            }

            return sb.ToString();
        }
    }
}
