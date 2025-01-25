using System;
using System.Text.RegularExpressions;
using EncodeCalculator.SuffixExpressionsCalculating;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Runner;

public class RunCalculator
{
    private static int TheNumberOfRecursions = 0; //递归次数统计
    private static string AlgorithmicProcess = "";
    public class RunnerClass
    {
        public string Formula { get; set; }= string.Empty;
        public string Result { get; set; }= string.Empty;
    }
    public static RunnerClass Run()
    {
        TheNumberOfRecursions = 0;//计算前清零递归次数
        AlgorithmicProcess = "";
        var Formula = GetFormula(ItemManage.ItemMange.GetValueForName("Main"));//获取计算表达式

        if (Formula == null)
        {
            throw new Exception("表达式中使用了死循环递归！");
        }
        
        double result = new FourCalculations().Compute(Formula);//计算
        return new RunnerClass()
        {
            Formula = Formula,
            Result = result.ToString(),
        };
    }

    public static bool DetermineWhetherAFunctionExists(string expression)//查找是否剩余函数
    {
        foreach (var Item in ItemManage.ItemMange.Items)
        {
            if (Item.Type == Type.Type.NodeType.Function)
            {
                if (expression.Contains($"{Item.FuncItem.Name}()"))
                {
                    return true;
                }
            }
            else
            {
                if (expression.Contains($"{Item.CompItem.Name}()"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public static string GetFormula(string formula)//获取表达式
    {
        if (TheNumberOfRecursions > 100)//判断递归次数
        {
            return string.Empty;
        }
        else
        {
            var result = formula;
            TheNumberOfRecursions++;
        
            foreach (var Item in ItemManage.ItemMange.Items)
            {
                if (Item.Type == Type.Type.NodeType.Function)
                {
                    var fors = ReplaceFunction(result, Item.FuncItem.Name, () => Item.FuncItem.Value); //取替换结果
            
                    if (fors != null)
                    {
                        result = fors;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Item.CompItem.Value1) && !string.IsNullOrEmpty(Item.CompItem.Value2))
                    {
                        var for1 = GetFormula(Item.CompItem.Value1);
                        var for2 = GetFormula(Item.CompItem.Value2);
                
                        Console.WriteLine(for1 + "," + for2);
                        result = null;
                    }
                }
            }
        
            if (!string.IsNullOrEmpty(result))
            {
                if (DetermineWhetherAFunctionExists(result))
                {
                    var res = GetFormula(result);
                    if (string.IsNullOrEmpty(res))
                    {
                        return null;
                    }
            
                    return GetFormula(result);//返回，true为有剩余，false为没有剩余
                }
            }
            return result;//返回
        }
    }
    private static string ReplaceFunction(string expression, string functionName, Func<string> replacement)//替换函数项为表达式
    {
        if (TheNumberOfRecursions > 100)//判断递归次数
        {
            return string.Empty;
        }
        else
        {
            try
            {
                var rep = replacement();
                expression = expression.Replace($"{functionName}()", rep);//替换函数项为表达式
                TheNumberOfRecursions++;
                return expression;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}