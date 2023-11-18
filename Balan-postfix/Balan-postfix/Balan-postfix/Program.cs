using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balan_postfix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhập biểu thức: ");
            string expression = Console.ReadLine();

            List<string> tokens = Tokenize(expression);
            List<string> postfixExpression = ConvertToPostfix(tokens);
            Console.WriteLine("Biểu thức chuẩn (Postfix): " + string.Join(" ", postfixExpression));

            Dictionary<string, double> variableValues = GetVariableValues(postfixExpression);
            double result = EvaluatePostfix(postfixExpression, variableValues);
            Console.WriteLine("Kết quả: " + result);
            if (IsParenthesesValid(expression))
        {
            List<string> tokens = Tokenize(expression);
            List<string> postfixExpression = ConvertToPostfix(tokens);

            double result = EvaluatePostfix(postfixExpression);
            Console.WriteLine("Kết quả: " + result);
        }
        else
        {
            Console.WriteLine("Biểu thức không hợp lệ");
        }
        }
        static List<string> Tokenize(string expression)
        {
            List<string> tokens = new List<string>();
            string currentToken = "";

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];

                if (char.IsDigit(c) || c == '.')
                {
                    currentToken += c;
                }
                else if (char.IsLetter(c))
                {
                    currentToken += c;
                    while (i + 1 < expression.Length && char.IsLetterOrDigit(expression[i + 1]))
                    {
                        currentToken += expression[i + 1];
                        i++;
                    }
                    tokens.Add(currentToken);
                    currentToken = "";
                }
                else if (!char.IsWhiteSpace(c))
                {
                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }
                    tokens.Add(c.ToString());
                }
            }

            if (!string.IsNullOrEmpty(currentToken))
            {
                tokens.Add(currentToken);
            }

            return tokens;
        }

        static List<string> ConvertToPostfix(List<string> tokens)
        {
            List<string> postfix = new List<string>();
            Stack<string> stack = new Stack<string>();

            foreach (string token in tokens)
            {
                if (IsNumber(token) || IsVariable(token))
                {
                    postfix.Add(token);
                }
                else if (IsOperator(token))
                {
                    while (stack.Count > 0 && IsOperator(stack.Peek()) && GetPrecedence(token) <= GetPrecedence(stack.Peek()))
                    {
                        postfix.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        postfix.Add(stack.Pop());
                    }

                    if (stack.Count == 0)
                    {
                        throw new ArgumentException("Biểu thức không hợp lệ: thiếu dấu mở ngoặc");
                    }

                    stack.Pop();
                }
            }

            while (stack.Count > 0)
            {
                if (stack.Peek() == "(")
                {
                    throw new ArgumentException("Biểu thức không hợp lệ: thiếu dấu đóng ngoặc");
                }
                postfix.Add(stack.Pop());
            }

            return postfix;
        }

        static double EvaluatePostfix(List<string> postfix)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in postfix)
            {
                if (IsNumber(token))
                {
                    stack.Push(double.Parse(token));
                }
                else if (IsVariable(token))
                {
                    Console.Write("Nhập giá trị cho biến " + token + ": ");
                    double value = double.Parse(Console.ReadLine());
                    stack.Push(value);
                }
                else if (IsOperator(token))
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();

                    double result = PerformOperation(token, operand1, operand2);
                    stack.Push(result);
                }
            }

            if (stack.Count != 1)
            {
                throw new ArgumentException("Biểu thức không hợp lệ");
            }

            return stack.Pop();
        }

        static bool IsNumber(string token)
        {
            return double.TryParse(token, out _);
        }

        static bool IsVariable(string token)
        {
            return char.IsLetter(token[0]);
        }

        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "%" || token == "^" || token == "!" || token == "log" || token == "sin" || token == "cos";
        }

        static double PerformOperation(string operatorToken, double operand1, double operand2)
        {
            switch (operatorToken)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                case "%":
                    return operand1 % operand2;
                case "^":
                    return Math.Pow(operand1, operand2);
                case "!":
                    return Factorial(operand1);
                case "log":
                    return Math.Log(operand1, operand2);
                case "sin":
                    return Math.Sin(operand1);
                case "cos":
                    return Math.Cos(operand1);
                default:
                    throw new ArgumentException("Phép toán không hợp lệ: " + operatorToken);
            }
        }

        static double Factorial(double n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }

        static int GetPrecedence(string operatorToken)
        {
            switch (operatorToken)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "%":
                    return 2;
                case "^":
                case "!":
                case "log":
                case "sin":
                case "cos":
                    return 3;
                default:
                    throw new ArgumentException("Phép toán không hợp lệ: " + operatorToken);
            }
        }
        static Dictionary<string, double> GetVariableValues(List<string> postfixExpression)
        {
            Dictionary<string, double> variableValues = new Dictionary<string, double>();

            foreach (string token in postfixExpression)
            {
                if (IsVariable(token))
                {
                    if (!variableValues.ContainsKey(token))
                    {
                        Console.Write("Nhập giá trị cho biến " + token + ": ");
                        double value = double.Parse(Console.ReadLine());
                        variableValues[token] = value;
                    }
                }
            }

            return variableValues;
        }
        static double EvaluatePostfix(List<string> postfix, Dictionary<string, double> variableValues)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in postfix)
            {
                if (IsNumber(token))
                {
                    stack.Push(double.Parse(token));
                }
                else if (IsVariable(token))
                {
                    if (variableValues.ContainsKey(token))
                    {
                        stack.Push(variableValues[token]);
                    }
                    else
                    {
                        throw new ArgumentException("Giá trị của biến " + token + " chưa được xác định");
                    }
                }
                else if (IsOperator(token))
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();

                    double result = PerformOperation(token, operand1, operand2);
                    stack.Push(result);
                }
            }

            if (stack.Count != 1)
            {
                throw new ArgumentException("Biểu thức không hợp lệ");
            }

            return stack.Pop();
        }
    }
}
