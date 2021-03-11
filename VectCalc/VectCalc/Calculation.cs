using System;
using System.Collections.Generic;
using System.Text;

namespace VectCalc
{
    public class Calculation
    {
        static public double GetResult(string input)
        {
            string output = ToRPN(input);
            double result = Calculate(output);
            return result;
        }

        static private string ToRPN(string input)
        {
            string rpn = string.Empty;
            Stack<char> operatorsStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;

                if (Char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        rpn += input[i];
                        i++;

                        if (i == input.Length) break;
                    }

                    rpn += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operatorsStack.Push(input[i]);

                    else if (input[i] == ')')
                    {
                        char s = operatorsStack.Pop();

                        while (s != '(')
                        {
                            rpn += s.ToString() + ' ';
                            s = operatorsStack.Pop();
                        }
                    }

                    else
                    {
                        if (operatorsStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operatorsStack.Peek()))
                                rpn += operatorsStack.Pop().ToString() + " ";

                        operatorsStack.Push(char.Parse(input[i].ToString()));

                    }
                }
            }

            while (operatorsStack.Count > 0)
                rpn += operatorsStack.Pop() + " ";

            return rpn;
        }

        static private double Calculate(string input)
        {
            double result = 0;
            Stack<double> temp = new Stack<double>();

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    string resStr = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        resStr += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(resStr));
                    i--;
                }

                else if (IsOperator(input[i]))
                {
                    double first = temp.Pop();
                    double second = temp.Pop();

                    switch (input[i])
                    {
                        case '+': result = second + first; 
                            break;
                        case '-': result = second - first; 
                            break;
                        case '*': result = second * first; 
                            break;
                        case '/': result = second / first; 
                            break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(second.ToString()), double.Parse(first.ToString())).ToString());
                            break;
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }

        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }

        static private bool IsOperator(char с)
        {
            if (("+-/*^()".IndexOf(с) != -1))
                return true;
            return false;
        }

        static private byte GetPriority(char c)
        {
            switch (c)
            {
                case '(': return 0;
                case ')': return 0;
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                case '^': return 3;
                default: return 10;
            }
        }
    }
}
