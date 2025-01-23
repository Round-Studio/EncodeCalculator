using System;
using System.Collections.Generic;

// 定义一个节点类，用于表示树形结构
public class TreeNode
{
    public string Value { get; set; } // 节点的值或操作符
    public TreeNode Left { get; set; } // 左子节点
    public TreeNode Right { get; set; } // 右子节点

    public TreeNode(string value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class ExpressionParser
{
    private int index = 0; // 当前解析的字符位置
    private string expression; // 输入的算式

    public ExpressionParser(string expr)
    {
        expression = expr.Replace(" ", ""); // 去除空格
    }

    // 解析整个表达式
    public TreeNode Parse()
    {
        index = 0; // 重置解析位置
        return ParseExpression();
    }

    // 解析加法和减法
    private TreeNode ParseExpression()
    {
        TreeNode left = ParseTerm();
        while (index < expression.Length)
        {
            char op = expression[index];
            if (op == '+' || op == '-')
            {
                index++; // 跳过操作符
                TreeNode right = ParseTerm();
                TreeNode node = new TreeNode(op.ToString());
                node.Left = left;
                node.Right = right;
                left = node;
            }
            else
            {
                break;
            }
        }
        return left;
    }

    // 解析乘法和除法
    private TreeNode ParseTerm()
    {
        TreeNode left = ParseFactor();
        while (index < expression.Length)
        {
            char op = expression[index];
            if (op == '*' || op == '/')
            {
                index++; // 跳过操作符
                TreeNode right = ParseFactor();
                TreeNode node = new TreeNode(op.ToString());
                node.Left = left;
                node.Right = right;
                left = node;
            }
            else
            {
                break;
            }
        }
        return left;
    }

    // 解析括号或数字
    private TreeNode ParseFactor()
    {
        if (expression[index] == '(')
        {
            index++; // 跳过左括号
            TreeNode node = ParseExpression();
            if (expression[index] == ')')
            {
                index++; // 跳过右括号
            }
            return node;
        }
        else
        {
            int number = 0;
            while (index < expression.Length && char.IsDigit(expression[index]))
            {
                number = number * 10 + (expression[index] - '0');
                index++;
            }
            return new TreeNode(number.ToString());
        }
    }
}

public class Program
{
    public static void Main()
    {
        string expression = "(77+230)-81923*115+(29/55)"; // 示例算式
        ExpressionParser parser = new ExpressionParser(expression);
        TreeNode root = parser.Parse();

        Console.WriteLine("算式分解树：");
        PrintTree(root, "");
    }

    // 打印树形结构
    public static void PrintTree(TreeNode node, string indent)
    {
        if (node == null) return;

        Console.WriteLine(indent + node.Value);
        PrintTree(node.Left, indent + "  |-- ");
        PrintTree(node.Right, indent + "  |-- ");
    }
}