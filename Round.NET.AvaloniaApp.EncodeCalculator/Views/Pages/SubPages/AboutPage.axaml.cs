using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Update;

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
}