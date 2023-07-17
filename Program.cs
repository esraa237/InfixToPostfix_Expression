using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infix_to_postfix_v1
{
    public class Program
    {
        public bool IsOperator(char ch)
        {
            if(ch== '+' || ch == '-' || ch == '*' || ch == '/')
            {
                return true;
            }
            return false;
        }
        public int priority(char ch)
        {
            if (ch == '-' || ch == '+') return 1;
            else if (ch == '*' || ch == '/') return 2;
            else return 0;
        }

        public void convert(ref string infix,out string postfix)
        {
            postfix = "";
            Stack<char> op_stack = new Stack<char>();
            for (int i = 0; i < infix.Length; i++)
            {
                char ch = infix[i];
                if (IsOperator(ch))
                {
                    if (op_stack.Count <= 0)
                    {
                        op_stack.Push(ch);
                    }
                    else
                    {
                        if (priority(ch) > priority(op_stack.Peek()))//example ch=* stack=+
                        {
                            op_stack.Push(ch);
                        }
                        else if(priority(ch) < priority(op_stack.Peek()))//example ch=+ stack=* 
                        {
                            postfix += op_stack.Pop();
                            i--;
                        }
                        else//example ch=/ stack=* or ch=- stack=+
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
            for (int j = 0; j <= op_stack.Count; j++)
                postfix += op_stack.Pop();

        } 
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the infix Expression: ");
            string infix = Console.ReadLine();
            string postfix = "";
            Program p=new Program();
            p.convert(ref infix,out postfix);
            System.Console.WriteLine("InFix  :  " + infix);
            System.Console.WriteLine("PostFix:  " + postfix);
            Console.ReadKey();
        }
    }
}
