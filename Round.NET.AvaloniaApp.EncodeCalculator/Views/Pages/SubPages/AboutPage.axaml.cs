using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Update;
using BackUpdate = Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls.BackUpdate;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Pages.SubPages;

public partial class AboutPage : UserControl
{
    public AboutPage()
    {
        InitializeComponent();
        VersionBox.Content = $"当前版本：{Update.GetCurrentVersion()}";
    }

    private void UpdateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Core.MainWindow.UpdateFunc(true);
    }

    private void BackUpdateButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ((ContentDialog)this.Parent).Hide();
        var show = new ContentDialog()
        {
            DefaultButton = ContentDialogButton.Close,
            CloseButtonText = "确定",
            Title = "更新回滚",
            Content = new BackUpdate()
        };
        show.ShowAsync(Core.MainWindow);
    }
}