using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        Core.OutBox = OutBox;
    }

    private void CopyButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var clipboard = Core.MainWindow.Clipboard;
        clipboard.SetTextAsync(OutBox.Text);
        Core.MainWindow.ShowMessage("复制结果","结果复制成功!",NotificationType.Success);
    }

    private void AboutButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            var Show = new ContentDialog();
            Show.Title = "关于计算模块";
            Show.CloseButtonText = "确定";
            Show.Content = $"计算模块：By Haveaovinopensourcespirit\n" +
                           $"所有权    ：Round Studio\n" +
                           $"";
            Show.DefaultButton = ContentDialogButton.Close;
            Show.ShowAsync(Core.MainWindow);
        });
    }

    private async void AutoSaveFileButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var name = "";
        if (Core.ProjectPath != string.Empty)
        {
            name = Path.GetFileName(Core.ProjectPath);
            Core.ModifyTheStatus = false;
        }
        else
        {
            name = $"{Core.ProjectName}";
        }
        var saveFileDialog = new SaveFileDialog
        {
            Title = "另存为项目文件",
            InitialFileName = name,
            DefaultExtension = Project.FILE_TYPE,
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "REC 项目文件", Extensions = new List<string> { Project.FILE_TYPE } }
            }
        };

        string? filePath = await saveFileDialog.ShowAsync(Core.MainWindow);

        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, Project.SaveProject.GetTheContentsOfTheSaveFile());
            Core.ProjectPath = filePath;
            Core.ModifyTheStatus = false;
        }
    }

    private async void OpenFileButton_OnClick(object? sender, RoutedEventArgs e)
    {
        async void Open()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "打开项目",
                AllowMultiple =false,
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter { Name = "REC 项目文件", Extensions = new List<string> { Project.FILE_TYPE } }
                }
            };

            // ShowAsync returns an array of selected file paths
            string[]? filePaths = await openFileDialog.ShowAsync(Core.MainWindow);

            try
            {
                if (File.Exists(filePaths[0]))
                {
                    if (filePaths != null)
                    {
                        Project.OpenProject.OpenProjectFile(filePaths[0]);
                        Core.ProjectPath = filePaths[0];
                        Core.ModifyTheStatus = false;
                    }
                }
            }catch { }
        }
        
        if (!Core.ModifyTheStatus)
        {
            Open();
        }
        else
        {
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.DefaultButton = ContentDialogButton.Close;
            contentDialog.PrimaryButtonText = "强制打开";
            contentDialog.SecondaryButtonText = "保存后打开";
            contentDialog.CloseButtonText = "我不打开";
            contentDialog.Title = "提示";
            contentDialog.Content = "当前文件未保存，请问您如何处理？";
            contentDialog.PrimaryButtonClick += (_, _) =>
            {
                Open();
            };
            contentDialog.SecondaryButtonClick += (_, _) =>
            {
                SaveFileButton_OnClick(sender, e);
                Open();
            };
            contentDialog.ShowAsync(Core.MainWindow);
        }
    }

    private async void SaveFileButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Core.ProjectPath != "" && Core.ProjectPath != string.Empty && Core.ProjectPath != null)
        {
            File.WriteAllText(Core.ProjectPath, Project.SaveProject.GetTheContentsOfTheSaveFile());
            Core.ModifyTheStatus = false;
        }
        else
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "保存项目文件",
                InitialFileName = $"{Core.ProjectName}",
                DefaultExtension = Project.FILE_TYPE,
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter { Name = "REC 项目文件", Extensions = new List<string> { Project.FILE_TYPE } }
                }
            };

            string? filePath = await saveFileDialog.ShowAsync(Core.MainWindow);

            if (!string.IsNullOrEmpty(filePath))
            {
                File.WriteAllText(filePath, Project.SaveProject.GetTheContentsOfTheSaveFile());
                Core.ProjectPath = filePath;
                Core.ModifyTheStatus = false;
            }
        }
    }

    private void NewFileButton_OnClick(object? sender, RoutedEventArgs e)
    {
        void New()
        {
            Project.NewProject.NewProjectCore();
        }
        
        if (!Core.ModifyTheStatus)
        {
            New();
        }
        else
        {
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.DefaultButton = ContentDialogButton.Close;
            contentDialog.PrimaryButtonText = "强制新建";
            contentDialog.SecondaryButtonText = "保存后新建";
            contentDialog.CloseButtonText = "我不新建";
            contentDialog.Title = "提示";
            contentDialog.Content = "当前文件未保存，请问您如何处理？";
            contentDialog.PrimaryButtonClick += (_, _) =>
            {
                New();
            };
            contentDialog.SecondaryButtonClick += (_, _) =>
            {
                SaveFileButton_OnClick(sender, e);
                New();
            };
            contentDialog.ShowAsync(Core.MainWindow);
        }
    }

    private void ExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        void Exit() => Environment.Exit(0); // 0表示正常退出，非0表示异常退出
        if (!Core.ModifyTheStatus)
        {
            Exit();
        }
        else
        {
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.DefaultButton = ContentDialogButton.Close;
            contentDialog.PrimaryButtonText = "强制退出";
            contentDialog.CloseButtonText = "我不退出";
            contentDialog.Title = "提示";
            contentDialog.Content = "当前文件未保存，请问您如何处理？";
            contentDialog.PrimaryButtonClick += (_, _) => { Exit(); };
            contentDialog.ShowAsync(Core.MainWindow);
        }
    }

    private void PropertyButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var path = Core.ProjectPath;
        if (path == string.Empty)
        {
            path = "null";
        }
        ContentDialog contentDialog = new ContentDialog();
        contentDialog.DefaultButton = ContentDialogButton.Close;
        contentDialog.CloseButtonText = "确定";
        contentDialog.Title = "项目属性";
        contentDialog.Content = new Grid()
        {
            Children =
            {
                new StackPanel()
                {
                    Children =
                    {
                        new Label()
                        {
                            Content = $"项目名称：{Core.ProjectName}",
                            Margin = new Thickness(5)
                        },
                        new Label()
                        {
                            Content = $"项目路径：{path}",
                            Margin = new Thickness(5)
                        }
                    }
                }
            }
        };
        contentDialog.ShowAsync(Core.MainWindow);
    }
}
