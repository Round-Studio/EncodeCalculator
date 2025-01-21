using System;
using System.Text.RegularExpressions;
using Avalonia.Threading;
using EncodeCalculator.SuffixExpressionsCalculating;
using FluentAvalonia.UI.Controls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Runner;

public class RunCalculator
{
    private static int TheNumberOfRecursions = 0; //递归次数统计
    private static string AlgorithmicProcess = "";
    public static void Run()
    {
        try
        {
            TheNumberOfRecursions = 0;//计算前清零递归次数
            AlgorithmicProcess = "";
            var Formula = GetFormula(ItemManage.ItemMange.GetValueForName("Main"));//获取计算表达式
            Core.OutBox.Text = $"算式：{Formula}";
            Console.Write(Formula);//调试输出
            double result = FourCalculations.Compute(Formula);//计算
            Console.WriteLine($"={result}");//临时输出
            Core.OutBox.Text+=$"\n结果：{result}";
        }
        catch (Exception ex) {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public static bool DetermineWhetherAFunctionExists(string expression)//查找是否剩余函数
    {
        foreach (var Item in ItemManage.ItemMange.Items)
        {
            if (expression.Contains($"{Item.Config.Name}()"))
            {
                return true;
            }
        }
        return false;
    }
    public static string GetFormula(string formula)//获取表达式
    {
        var result = formula;
        
        foreach (var Item in ItemManage.ItemMange.Items)
        {
            result = ReplaceFunction(result, Item.Config.Name, () => Item.ValueBox.Text); //取替换结果
        }
        
        if (DetermineWhetherAFunctionExists(result))
        {
            return GetFormula(result);//返回，true为有剩余，false为没有剩余
        }
        return result;//返回
    }
    private static string ReplaceFunction(string expression, string functionName, Func<string> replacement)//替换函数项为表达式
    {
        if (TheNumberOfRecursions > 100)//判断递归次数
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                var Show = new ContentDialog();
                Show.Title = "运行错误";
                Show.Content = $"发生死亡性运行错误！\n请不要在你的表达式中使用死循环递归！";
                Show.CloseButtonText = "确定";
                Show.DefaultButton = ContentDialogButton.Close;
                Show.ShowAsync(Core.MainWindow);
            });
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