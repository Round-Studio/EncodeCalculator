using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class EditItem : UserControl
{
    public ItemMange.RootConfig Config;
    private bool Chang = false;
    public ContentDialog ContentDialog;
    public EditItem(ItemMange.RootConfig Config)
    {
        this.Config = Config;
        InitializeComponent();

        if (Config.IsMain)
        {
            NameBox.IsEnabled = false;
            DeleteItem.IsEnabled = false;
        }
        NameBox.Text = Config.Name;
        ValueBox.Text = Config.Value;
        Chang = true;
    }

    private void ValueBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        if (Chang)
        {
            Config.Value = ValueBox.Text;   
        }
    }

    private void NameBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        if (Chang)
        {
            Config.Name = NameBox.Text; 
        }
    }

    private void DeleteItem_OnClick(object? sender, RoutedEventArgs e)
    {
        ItemMange.DeleteItemForUUID(Config.UUID);
        ContentDialog.Hide();
    }
}