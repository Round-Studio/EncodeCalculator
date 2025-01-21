using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Runner;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class ItemsView : UserControl
{
    public ItemsView()
    {
        InitializeComponent();
        ItemMange.ItemListBox = this.ItemListBox;
        ItemMange.AddItem(new ItemMange.RootConfig()
        {
            Value = "1+1",
            Name = "Main",
            IsMain = true,
        });
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
            }
        };
        
        Show.ShowAsync(Core.MainWindow);
    }

    private void RunButton_OnClick(object? sender, RoutedEventArgs e)
    {
        RunCalculator.Run();
    }
}