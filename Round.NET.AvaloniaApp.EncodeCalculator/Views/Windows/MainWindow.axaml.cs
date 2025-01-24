using System;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using EncodeCalculator.SuffixExpressionsCalculating;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Config;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views;

public partial class MainWindow : Window
{
    public WindowNotificationManager? _manager;
    public MainWindow()
    {
        Config.LoadConfig();
        
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
        _manager.Show(new Notification(message, title, type));
    }
}
