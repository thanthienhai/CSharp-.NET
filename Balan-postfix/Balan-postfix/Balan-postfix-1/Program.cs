using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balan_postfix
{
    class Program
    {
        // Hàm này kiểm tra xem hai ký tự mở và đóng có tương ứng nhau không
        static bool IsPair(char open, char close)
        {
            if (open == '(' && close == ')')
            {
                return true;
            }
            else if (open == '{' && close == '}')
            {
                return true;
            }
            else if (open == '[' && close == ']')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Hàm này kiểm tra biểu thức có cân bằng hay không
        static bool IsBalanced(string expression)
        {
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(' || expression[i] == '[' || expression[i] == '{')
                {
                    stack.Push(expression[i]);
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }
                    else if (IsPair(stack.Peek(), expression[i]))
                    {
                        stack.Pop();
                    }
                }
            }
            return stack.Count == 0;
        }
            // Hàm này trả về độ ưu tiên của phép toán
            static int Priority(char operation)
        {
            if (operation == '+' || operation == '-')
            {
                return 1;
            }
            else if (operation == '*' || operation == '/')
            {
                return 2;
            }
            else if (operation == '^')
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }

        // Hàm này chuyển đổi biểu thức trung tố thành hậu tố
        static string InfixToPostfix(string expression)
        {
            Stack<char> stack = new Stack<char>();
            string postfix = "";

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ' ')
                {
                    continue;
                }
                else if (Char.IsDigit(expression[i]) || Char.IsLetter(expression[i]))
                {
                    postfix += expression[i];
                }
                else if (expression[i] == '(' || expression[i] == '{' || expression[i] == '[' || expression[i] == '+' || expression[i] == '*' || expression[i] == '/' || expression[i] == '-')
                {
                    stack.Push(expression[i]);
                }
                else if (expression[i] == ')')
                {
                    while (stack.Peek() != '(')
                    {
                        postfix += stack.Pop();
                    }
                    stack.Pop();
                }
                else if (expression[i] == '}')
                {
                    while (stack.Peek() != '{')
                    {
                        postfix += stack.Pop();
                    }
                    stack.Pop();
                }
                else if (expression[i] == ']')
                {
                    while (stack.Peek() != '[')
                    {
                        postfix += stack.Pop();
                    }
                    stack.Pop();
                }
                else
                {
                    while (stack.Count > 0 && Priority(expression[i]) <= Priority(stack.Peek()))
                    {
                        postfix += stack.Pop();
                    }
                    stack.Push(expression[i]);
                }
            }

            while (stack.Count > 0)
            {
                postfix += stack.Pop();
            }

            return postfix;
        }

        static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            if (!IsBalanced(expression))
            {
                Console.WriteLine("the expression is not Balanced");
            }
            else
            {
                Console.WriteLine("the expression is Balanced");
                Console.WriteLine(InfixToPostfix(expression));
            }
            Console.ReadKey();
        }
    }
}