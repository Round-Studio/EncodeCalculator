using EncodeCalculator.SuffixExpressionsCalculating.Exception;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace EncodeCalculator.SuffixExpressionsCalculating
{

    /// <summary>
    /// 用于四则计算的类
    /// </summary>
    public class FourCalculations
    {

        /// <summary>
        /// 依赖注入中的构造函数注入，将计算过程依赖类注入至其中。
        /// </summary>
        /// <param name="cp">计算过程类</param>
        public FourCalculations(CalculationProcess cp) {
            if (cp == null) ArgumentNullException.ThrowIfNull(cp);
            this.cp = cp;
        }

        public FourCalculations() { cp = new CalculationProcess(); }

        /// <summary>
        /// 用于存储后缀表达式
        /// </summary>
        public static string? Suffix { get; set; }

        /// <summary>
        /// 用于存储中缀表达式
        /// </summary>
        public static string? Infix { get; set; }

        private CalculationProcess cp;

        /// <summary>
        /// 计算算术表达式（主要用于测试，不建议拿来使用）
        /// 因为此方法可以重写
        /// </summary>
        /// <param name="refs">算式</param>
        /// <returns>计算后的结果</returns>
        /// <exception cref="NullReferenceException">存储后缀表达式意外为空</exception>
        public virtual double Compute(string refs) {
            double result = 0.0;
            this.ConvertSuffixExpressions(refs);
            if (FourCalculations.Suffix == null) throw new NullReferenceException("Suffix意外为NULL");
            string p;
            result = this.EvaluatePostfix(FourCalculations.Suffix);
            //Console.WriteLine("\n\n\n计算过程如下：\n");
            //foreach(var i in cp)
            return result;
        }
        
        /// <summary>
        /// 逆波兰操作，将中缀转换成后缀
        /// </summary>
        /// <param name="infix">中缀表达式</param>
        /// <returns>后缀表达式</returns>
        /// <exception cref="ArgumentNullException">中缀表达式为空</exception>
        /// <exception cref="ParseInvalidException">遇到无效符号</exception>
        /// 
        public string ConvertSuffixExpressions(string infix) {
            Infix = infix;
            Stack<char> stack = new Stack<char>();
            List<string> output = new List<string>();
            Dictionary<char, int> precedence = new Dictionary<char, int>
            {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 }
        };

            StringBuilder numberBuffer = new StringBuilder();

            foreach (char c in infix) {
                if (char.IsDigit(c) || c == '.') {
                    numberBuffer.Append(c);
                }
                else {
                    if (numberBuffer.Length > 0) {
                        output.Add(numberBuffer.ToString());
                        numberBuffer.Clear();
                    }

                    if (c == '(') {
                        stack.Push(c);
                    }
                    else if (c == ')') {
                        while (stack.Count > 0 && stack.Peek() != '(') {
                            output.Add(stack.Pop().ToString());
                        }
                        stack.Pop(); // 弹出左括号
                    }
                    else if (precedence.ContainsKey(c)) {
                        while (stack.Count > 0 && precedence.ContainsKey(stack.Peek()) && precedence[stack.Peek()] >= precedence[c]) {
                            output.Add(stack.Pop().ToString());
                        }
                        stack.Push(c);
                    }
                    else {
                        throw new ArgumentException($"无效符号：{c}");
                    }
                }
            }

            if (numberBuffer.Length > 0) {
                output.Add(numberBuffer.ToString());
            }

            while (stack.Count > 0) {
                if (stack.Peek() == '(' || stack.Peek() == ')') {
                    throw new ArgumentException("括号不匹配");
                }
                output.Add(stack.Pop().ToString());
            }

            Suffix = string.Join(" ", output);
            return Suffix;
        }

        /*        public static string ConvertSuffixExpressions(string infix) {
                    Stack<char> stack = new Stack<char>();
                    List<string> output = new List<string>(); // 存储后缀表达式
                    Dictionary<char, int> precedence = new Dictionary<char, int>
                    {
                { '+', 1 },
                { '-', 1 },
                { '*', 2 },
                { '/', 2 },
                { '%', 2 },
                { '^', 3 }
            };

                    // 用于处理多位数
                    StringBuilder numberBuffer = new StringBuilder();

                    foreach (char c in infix) {
                        if (char.IsDigit(c) || c == '.') {
                            // 如果是数字或小数点，累加到 numberBuffer
                            numberBuffer.Append(c);
                        }
                        else {
                            // 如果不是数字，且 numberBuffer 不为空，将数字添加到输出列表
                            if (numberBuffer.Length > 0) {
                                output.Add(numberBuffer.ToString());
                                numberBuffer.Clear();
                            }

                            if (c == '(') {
                                stack.Push(c);
                            }
                            else if (c == ')') {
                                while (stack.Count > 0 && stack.Peek() != '(') {
                                    output.Add(stack.Pop().ToString());
                                }
                                if (stack.Count > 0 && stack.Peek() == '(') {
                                    stack.Pop(); // 弹出左括号
                                }
                            }
                            else if (precedence.ContainsKey(c)) {
                                while (stack.Count > 0 && precedence.ContainsKey(stack.Peek()) && precedence[stack.Peek()] >= precedence[c]) {
                                    output.Add(stack.Pop().ToString());
                                }
                                stack.Push(c);
                            }
                            else {
                                throw new ArgumentException($"无效符号：{c}");
                            }
                        }
                    }

                    // 如果 numberBuffer 不为空，将最后一个数字添加到输出列表
                    if (numberBuffer.Length > 0) {
                        output.Add(numberBuffer.ToString());
                    }

                    // 将栈中剩余的运算符依次弹出
                    while (stack.Count > 0) {
                        if (stack.Peek() == '(' || stack.Peek() == ')') {
                            throw new ArgumentException("括号不匹配");
                        }
                        output.Add(stack.Pop().ToString());
                    }
                    FourCalculations.Suffix = string.Join(" ", output);
                    return string.Join(" ", output); // 使用空格分隔后缀表达式
                }*/


        /// <summary>
        /// 计算后缀表达式
        /// </summary>
        /// <param name="postfix">后缀表达式</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public double EvaluatePostfix(string postfix) {
            Stack<double> stack = new Stack<double>();
            List<string> processList = new List<string>();

            string[] tokens = postfix.Split(' ');
            int i = 1;
            processList.Add($"原式子为：{postfix}");
            Node node1 = new Node();
            node1.Title = $"计算步骤_{i}     （分析原算式）";
            node1.Formula = Infix;
            node1.FormulaSuffix = postfix;

            Func<string> func1 = () => {
                string s = $"解：\t原式 = {node1.Formula}";
                if (node1.Formula == null || node1.Formula == string.Empty)
                    ArgumentNullException.ThrowIfNull(nameof(node1.Formula));
                if (node1.Formula.Any(c => c == '('))
                    s += "  \n\t∵ 原式有括号，括号改变了优先级\n";
                s += "∴ \t先算括号内";
                return s;
            };
            Func<string> func2;
            node1.Process = func1();
            cp.Add(node1);
            foreach (string token in tokens) {
                if (double.TryParse(token, out double number)) {
                    stack.Push(number);
                }
                else if (IsOperator(token[0])) {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();
                    double result = PerformOperation(token[0], operand1, operand2);
                    stack.Push(result);

                    Node node2 = new Node();

                    if (token[0] == '^') {
                        node2.Title = $"计算步骤_{++i}    (次幂\\次方的运算)";
                        //  processList.Add($"∵ {operand1}^{operand2} = {result}");
                        func2 = () => {
                            string s = string.Empty;
                            s = $"∵ \t待计算式子是{operand1}的{operand2}次幂运算\n";
                            s += $"∴ \t得到式子   {operand1}^{operand2}";
                            s += $"\n∴\t{operand1}^{operand2} = {result} ";
                            return s;
                        };
                        node2.Formula = $"{operand1}^{operand2}";
                        node2.Process = func2();
                        cp.Add(node2);
                    }
                    else {
                        node2.Title = $"计算步骤_{++i}";
                        // processList.Add($"∵ {operand1} {token[0]} {operand2} = {result}");

                        func2 = () => {
                            string s = $"∵ \t待计算式子是{operand1} {token[0]} {operand2}";
                            s += $"\n∴ \n {operand1} {token[0]} {operand2} = {result}";
                            return s;
                        };
                        node2.Process = func2();
                        cp.Add(node2);
                    }
                }
            }

            return stack.Pop();
        }

        /*   public static double EvaluatePostfix(string postfix) {
               Stack<double> stack = new Stack<double>();
               string[] tokens = postfix.Split(' '); // 按空格分割后缀表达式

               foreach (string token in tokens) {
                   if (double.TryParse(token, out double number)) {
                       // 如果是数字，压入栈
                       stack.Push(number);
                   }
                   else if (IsOperator(token[0])) // 假设运算符为单字符
                   {
                       // 如果是运算符，弹出两个操作数
                       if (stack.Count < 2) {
                           throw new InvalidOperationException("Invalid postfix expression.");
                       }
                       double operand2 = stack.Pop();
                       double operand1 = stack.Pop();
                       double result = PerformOperation(token[0], operand1, operand2);
                       stack.Push(result);
                   }
                   else {
                       throw new ArgumentException($"无效字符：{token}");
                   }
               }

               // 最终栈中应该只剩下一个元素
               if (stack.Count != 1) {
                   throw new InvalidOperationException("Invalid postfix expression.");
               }

               return stack.Pop();
           }*/




        /// <summary>
        /// 判断是否有效字符
        /// </summary>
        /// <param name="c">需判断的字符</param>
        /// <returns>是否有效</returns>
        protected static bool IsOperator(char c) {

            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '%';
        }
        
        /// <summary>
        /// 根据运算符执行相应的运算，并返回结果。
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operand1">操作数1</param>
        /// <param name="operand2">操作数2</param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected static double PerformOperation(char op, double operand1, double operand2) {
            switch (op) {
                case '+': return operand1 + operand2;
                case '-': return operand1 - operand2;
                case '*': return operand1 * operand2;
                case '/':
                    if (operand2 == 0)
                        throw new DivideByZeroException("Division by zero.");
                    return operand1 / operand2;
                case '^': return Math.Pow(operand1, operand2);
                case '%': return operand1 % operand2;
                default: throw new ArgumentException($"Invalid operator: {op}");
            }
        }

    }
}
