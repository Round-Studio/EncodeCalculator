using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class EditItem : UserControl
{
    private bool Chang = false;
    public ContentDialog ContentDialog;
    public UnitItem UnitItem;
    public EditItem(string uuid)
    {
        InitializeComponent();
        UnitItem = ItemMange.GetItem(uuid);
        if (this.UnitItem.IsMain)
        {
            NameBox.IsEnabled = false;
            DeleteItem.IsEnabled = false;
        }
        NameBox.Text = this.UnitItem.Name;
        ValueBox.Text = this.UnitItem.Value;
        Chang = true;
    }

    private void ValueBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        if (Chang)
        {
            this.UnitItem.Value = ValueBox.Text;   
        }
    }

    private void NameBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        if (Chang)
        {
            this.UnitItem.Name = NameBox.Text; 
        }
    }

    private void DeleteItem_OnClick(object? sender, RoutedEventArgs e)
    {
        ItemMange.DeleteItemForUUID(this.UnitItem.uuid);
        Core.ModifyTheStatus = true;
        ContentDialog.Hide();
    }
}