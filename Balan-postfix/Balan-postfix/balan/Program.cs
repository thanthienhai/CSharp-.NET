using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace balan
{
    class Program
    {
        static bool convert(ref string infix, out string postfix)
        {
            int prio = 0;
            postfix = "";
            Stack<char> s1 = new Stack<char>();
            for (int i = 0; i < infix.Length; i++)
            {
                char ch = infix[i];
                if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                {
                    if (s1.Count <= 0)
                        s1.Push(ch);
                    else
                    {
                        if (s1.Peek() == '*' || s1.Peek() == '/')
                            prio = 1;
                        else
                            prio = 0;
                        if (prio == 1)
                        {
                            if (ch == '+' || ch == '-')
                            {
                                postfix += s1.Pop();
                                i--;
                            }
                            else
                            {
                                postfix += s1.Pop();
                                i--;
                            }
                        }
                        else
                        {
                            if (ch == '+' || ch == '-')
                            {
                                postfix += s1.Pop();
                                s1.Push(ch);
                            }
                            else
                                s1.Push(ch);
                        }
                    }
                }
                else if (ch == '(' || ch == '{' || ch == '[')
                {
                    s1.Push(ch);
                }
                else if (ch == ')' || ch == '}' || ch == ']')
                {
                    while (s1.Count > 0 && (s1.Peek() != '(' && s1.Peek() != '{' && s1.Peek() != '['))
                    {
                        postfix += s1.Pop();
                    }
                    if (s1.Count == 0 || ((ch == ')' && s1.Peek() != '(') || (ch == '}' && s1.Peek() != '{') || (ch == ']' && s1.Peek() != '[')))
                    {
                        // Thông báo lỗi - dấu ngoặc không khớp
                        return false;
                    }
                    s1.Pop();
                }
                else
                {
                    postfix += ch;
                }
            }

            while (s1.Count > 0)
            {
                if (s1.Peek() == '(' || s1.Peek() == '[' || s1.Peek() == '{')
                {
                    // Thông báo lỗi - dấu ngoặc không khớp
                    return false;
                }
                postfix += s1.Pop();
            }

            return true;
        }
        static void Main(string[] args)
        {
            string infix = "";
            string postfix = "";
            if (args.Length == 1)
            {
                infix = args[0];
                convert(ref infix, out postfix);
                System.Console.WriteLine("InFix  :\t" + infix);
                System.Console.WriteLine("PostFix:\t" + postfix);
            }
            else
            {
                infix = "a+b*c-d";
                convert(ref infix, out postfix);
                System.Console.WriteLine("InFix   :\t" + infix);
                System.Console.WriteLine("PostFix :\t" + postfix);
                System.Console.WriteLine();
            }
        }
    }
}
