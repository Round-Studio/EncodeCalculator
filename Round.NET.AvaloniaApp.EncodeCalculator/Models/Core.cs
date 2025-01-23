using System;
using System.IO;
using System.Runtime.CompilerServices;
using AvaloniaEdit;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;
using Round.NET.AvaloniaApp.EncodeCalculator.Views;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models;

public class Core
{
    public static MainWindow MainWindow { get; set; }
    public static TextEditor OutBox { get; set; }

    private static string _ProjectPath { get; set; }
    private static string _ProjectName { get; set; }
    public static string ProjectPath
    {
        get
        {
            return _ProjectPath;
        }
        set
        {
            _ProjectPath = value;
            ProjectName = Path.GetFileName(value);
        }
    }
    public static string ProjectName
    {
        get
        {
            return _ProjectName;
        }
        set
        {
            if (value != String.Empty)
            {
                try
                {
                    _ProjectName = value;
                    RefreshTitle();
                }
                catch { }
            }
        }
    }

    private static bool _ModifyTheStatus {get; set;} = false;
    private static bool ModifyTheStatus
    {
        get
        {
            return _ModifyTheStatus;
        }
        set
        {
            _ModifyTheStatus = value;
            try
            {
                RefreshTitle();
            }
            catch { }
        }
    }

    public static void SetOutBoxText(string text)
    {
        OutBox.Text = text;
    }

    public static void RefreshTitle()
    {
        if (_ModifyTheStatus)
        {
            MainWindow.Title = $"REC - 可编码计算器 - *[{Core.ProjectName}]";
        }
        else
        {
            MainWindow.Title = $"REC - 可编码计算器 - [{Core.ProjectName}]";
        }
    }

    public static void SetNowModifyTheStatus(bool NowModifyTheStatus)
    {
        if (!Edit.Edit.EditMode)
        {
            ModifyTheStatus = NowModifyTheStatus;
            if (NowModifyTheStatus)
            {
                Edit.Edit.UndoStack.Push(Project.SaveProject.GetNowRoot());
                Console.WriteLine("添加了一次撤销");   
            }
        }
    }

    public static bool GetNowModifyTheStatus()
    {
        return ModifyTheStatus;
    }
}