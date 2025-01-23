using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Runner;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Pages.SubPages;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class ItemsView : UserControl
{
    public ItemsView()
    {
        InitializeComponent();
        ItemMange.ItemListBox = this.ItemListBox;
        
        Project.NewProject.NewProjectCore();
    }

    private void AddNewItemButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var AddItems = new AddItem();
        
        ContentDialog Show = new ContentDialog();
        Show.Title = "添加新项";
        Show.PrimaryButtonText = "添加";
        Show.CloseButtonText = "取消";
        Show.Content = AddItems;
        
        Show.DefaultButton = ContentDialogButton.Primary;
        Show.PrimaryButtonClick += (dialog, args) =>
        {
            if (!string.IsNullOrWhiteSpace(AddItems.ValueBox.Text))
            {
                ItemMange.AddItem(new ItemMange.RootConfig()
                {
                    Value = AddItems.ValueBox.Text,
                    Name = AddItems.NameBox.Text,
                });

                Core.ModifyTheStatus = true;
            }
        };
        
        Show.ShowAsync(Core.MainWindow);
    }

    private void RunButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            RunCalculator.Run();
        }
        catch
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                var Show = new ContentDialog();
                Show.Title = "运行错误";
                Show.Content = $"发生死亡性运行错误！\n请不要在你的表达式中使用死循环递归！";
                Show.CloseButtonText = "确定";
                Show.DefaultButton = ContentDialogButton.Close;
                Show.ShowAsync(Core.MainWindow);
            });
        }
    }

    private void AboutButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            var Show = new ContentDialog();
            Show.Title = "关于此软件";
            Show.Content = new AboutPage();
            Show.CloseButtonText = "确定";
            Show.DefaultButton = ContentDialogButton.Close;
            Show.ShowAsync(Core.MainWindow);
        });
    }

    private void SettingButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            var Show = new ContentDialog();
            Show.Title = "设置";
            Show.Content = new SettingPage();
            Show.CloseButtonText = "确定";
            Show.PrimaryButtonText = "取消";
            Show.DefaultButton = ContentDialogButton.Close;
            Show.ShowAsync(Core.MainWindow);
        });
    }
}