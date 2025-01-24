using System;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using EncodeCalculator.SuffixExpressionsCalculating;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Config;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Mange.TaskMange;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views;

public partial class MainWindow : Window
{
    public WindowNotificationManager? _manager;
    public MainWindow()
    {
        Config.LoadConfig();
        TaskCore.InitTaskCore();
        
        InitializeComponent();
        _manager = new WindowNotificationManager(this)
        {
            MaxItems = 2
        };
        _manager.Position = NotificationPosition.BottomRight;
        Core.MainWindow = this;
        this.Title = $"REC - 可编码计算器 - [{Project.DEFAULT_FILE_NAME}]";
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
}
