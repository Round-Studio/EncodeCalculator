using System;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using EncodeCalculator.SuffixExpressionsCalculating;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views;

public partial class MainWindow : Window
{
    public WindowNotificationManager? _manager;
    public MainWindow()
    {
        InitializeComponent();
        _manager = new WindowNotificationManager(this) { MaxItems = 1 };
        _manager.Position = NotificationPosition.BottomRight;
        Core.MainWindow = this;
    }

    public void ShowMessage(string message, string title, NotificationType type = NotificationType.Information)
    {
        _manager.Show(new Notification(message, title, type));
    }
}
