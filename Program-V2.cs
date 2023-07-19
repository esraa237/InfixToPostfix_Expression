using System;
using System.Collections.Generic;


namespace infix_to_posfix_c_shap
{
    internal class Program
    {
        //method to check the character is operator or not
        public bool IsOperator(char ch)
        {
            if (ch == '-' || ch == '/' || ch == '+' || ch == '*' || ch == '(' || ch == ')')
            {
                return true;
            }
            return false;
        }

        //method to set the priority of operators
        public int priority(char ch)
        {
            if (ch == '-' || ch == '+') return 1;
            else if (ch == '*' || ch == '/') return 2;
            else return 0;
        }
        public void convert(ref string infix, out string postfix)
        {
            postfix = "";
            Stack<char> op_stack = new Stack<char>(); //stack for operators
            for (int i = 0; i < infix.Length; i++)
            {
                char ch = infix[i];
                if (IsOperator(ch))
                {
                    if (op_stack.Count == 0) //if stack empty
                    {
                        op_stack.Push(ch);
                    }
                    else
                    {
                        if (ch == '(')
                        {
                            op_stack.Push(ch);
                        }
                        else if (ch == ')')
                        {
                            while (op_stack.Peek() != '(' && op_stack.Count > 0)
                            {
                                postfix += op_stack.Pop();
                            }
                            op_stack.Pop();
                        }
                        else if (priority(ch) > priority(op_stack.Peek()))//ex: ch:* , stack:+
                        {
                            op_stack.Push(ch);
                        }
                        else if (priority(ch) < priority(op_stack.Peek())) //ex: ch:+ ,stack:*
                        {
                            postfix += op_stack.Pop();
                            i--;
                        }

                        else //ex: ch:+ ,stack:-
                        {
                            postfix += op_stack.Pop();
                            op_stack.Push(ch);
                        }
                    }
                }
                else
                {
                    postfix += ch;
                }
            }
            //handle spaces
            if (op_stack.Count != 0)
            {
                for (int j = 0; j <= op_stack.Count; j++)
                {
                    postfix += op_stack.Pop();
                }
            }
            for (int i = 1; i < postfix.Length; i++)
            {
                if ((postfix[i - 1] == '*' || postfix[i - 1] == '/') && (postfix[i] == '-' || postfix[i] == '+'))
                {
                    string a = postfix.Substring(0, i);
                    string b = postfix.Substring(i);
                    postfix = a + " " + b;

                }
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter The Infix Expression : ");
            string infix = Console.ReadLine();
            string postfix = "";
            Program obj = new Program();
            obj.convert(ref infix, out postfix);
            Console.WriteLine("Infix :  " + infix);
            Console.WriteLine("Postfix :  " + postfix);
            Console.ReadKey();
        }
    }
}
