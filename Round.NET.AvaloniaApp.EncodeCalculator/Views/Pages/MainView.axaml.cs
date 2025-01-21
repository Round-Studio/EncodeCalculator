using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
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
}
