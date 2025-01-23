using System;
using System.Collections.Generic;
using Avalonia.Controls.Notifications;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Edit;

public class Edit
{
    public static Stack<Project.Root> UndoStack { get; set; } = new Stack<Project.Root>();
    public static Stack<Project.Root> RedoStack { get; set; } = new Stack<Project.Root>();
    public static bool EditMode { get; set; } = true;
    public static void UndoEdit()
    {
        EditMode = true;
        if (UndoStack.Count == 0)
        {
            Core.MainWindow.ShowMessage("编辑","没有可撤销的操作！",NotificationType.Error);
            EditMode = false;
            return;
        }

        Core.MainWindow.ShowMessage("编辑","撤销成功！已撤销 1 步",NotificationType.Success);
        RedoStack.Push(UndoStack.Pop());
        try
        {
            Project.OpenProject.OpenProjectCore(RedoStack.Peek());
        }catch{}
        EditMode = false;
    }

    public static void RedoEdit()
    {
        EditMode = true;
        if (RedoStack.Count == 0)
        {
            Core.MainWindow.ShowMessage("编辑","没有可重做的操作！",NotificationType.Error);
            EditMode = false;
            return;
        }

        Core.MainWindow.ShowMessage("编辑","重做成功！已重做 1 步",NotificationType.Success);
        UndoStack.Push(RedoStack.Pop());
        try{
            Project.OpenProject.OpenProjectCore(UndoStack.Peek());
        }catch{}
        EditMode = false;
    }

    public static void ClearEdits()
    {
        UndoStack.Clear();
        RedoStack.Clear();
    }
}