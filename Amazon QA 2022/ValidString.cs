using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class ValidString
    {

        // valid string, similar as valid parenthesis
        public bool IsValid(String s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                if (stack.Count == 0 || c != stack.Peek())
                {
                    stack.Push(c);
                }
                else if (c == stack.Peek())
                {
                    stack.Pop();
                }
            }

            return stack.Count == 0;

        }
    }
}
