using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using EncodeCalculator.SuffixExpressionsCalculating;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Config;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Mange.TaskMange;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Update;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views;

public partial class MainWindow : Window
{
    public WindowNotificationManager? _manager;
    public MainWindow()
    {
        Config.LoadConfig();
        TaskCore.InitTaskCore();
        
        InitializeComponent();
        if (Config.MainConfig.AutomaticUpdates)
        {
            UpdateFunc();
        }
        
        _manager = new WindowNotificationManager(this)
        {
            MaxItems = 2
        };
        _manager.Position = NotificationPosition.BottomRight;
        Core.MainWindow = this;
        this.Title = $"REC - 可编码计算器 - [{Project.DEFAULT_FILE_NAME}]";
    }

    public void UpdateFunc(bool abo = false)
    {
        Task.Run(() =>
        {
            if (Update.GetUpdate())
            {
                Dispatcher.UIThread.Invoke(() =>
                {
                    // Update.UpdateCore();   

                    ContentDialog sh = new ContentDialog();
                    sh.Title = "更新";
                    sh.DefaultButton = ContentDialogButton.Close;
                    sh.PrimaryButtonText = "取消";
                    sh.CloseButtonText = "确定";
                    sh.Content = new StackPanel()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Content = "您好！我们需要花费您一些时间以完成此次更新！"
                            },
                            new Label()
                            {
                                Content = $"当前版本：{Update.GetCurrentVersion()}"
                            },
                            new Label()
                            {
                                Content = $"更新版本：{Update.GetNewVersion()}"
                            },
                            new Label()
                            {
                                Content = $"更新时间：{Update.GetNewVersionTime()}"
                            }
                        }
                    };
                    sh.CloseButtonClick += (_, __) =>
                    {
                        var shc = new ContentDialog();
                        shc.Title = "更新";
                        shc.Content = new Controls.Update();
                        shc.ShowAsync(this);
                    };
                    sh.ShowAsync(this);
                });
            }
            else
            {
                if (abo)
                {
                    Dispatcher.UIThread.Invoke(() =>
                    {
                        // Update.UpdateCore();   

                        ContentDialog sh = new ContentDialog();
                        sh.Title = "更新";
                        sh.DefaultButton = ContentDialogButton.Close;
                        sh.CloseButtonText = "确定";
                        sh.Content = @"当前已是最新版本！";
                        sh.ShowAsync(this);
                    });
                }
            }
        });
    }
    public void ShowMessage(string message, string title, NotificationType type = NotificationType.Information)
    {
        bool fs = true;
        switch (type)
        {
            case NotificationType.Error:
                if (!Config.MainConfig.MessageModel[0])
                {
                    fs = false;
                }
                break;
            case NotificationType.Warning:
                if (!Config.MainConfig.MessageModel[1])
                {
                    fs = false;
                }
                break;
            case NotificationType.Success:
                if (!Config.MainConfig.MessageModel[2])
                {
                    fs = false;
                }
                break;
            case NotificationType.Information:
                if (!Config.MainConfig.MessageModel[2])
                {
                    fs = false;
                }
                break;
        }

        if (fs)
        {
            _manager.Show(new Notification(message, title, type));
        }
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        Environment.Exit(0); // 参数0表示正常退出
    }
}
