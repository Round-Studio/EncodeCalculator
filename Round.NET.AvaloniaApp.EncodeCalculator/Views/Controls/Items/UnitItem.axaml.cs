using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class UnitItem : UserControl
{
    public ItemMange.RootConfig Config;
    public UnitItem(ItemMange.RootConfig config)
    {
        InitializeComponent();
        this.Config = config;
        ValueBox.Text = config.Value;
        NameBox.Content = config.Name;
    }

    private void MoreButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            var Show = new ContentDialog();
            var Edit = new EditItem(Config);
            Edit.ContentDialog = Show;
            Show.Title = "编辑项";
            Show.CloseButtonText = "确定";
            Show.PrimaryButtonText = "取消";
            Show.Content = Edit;
            Show.DefaultButton = ContentDialogButton.Close;
            Show.CloseButtonClick += (_, __) =>
            {
                this.Config = Edit.Config;
                ValueBox.Text = this.Config.Value;
                NameBox.Content = this.Config.Name;
            };
            Show.ShowAsync(Core.MainWindow);
        });
    }

    private void ValueBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        Config.Value = ValueBox.Text;
    }
}