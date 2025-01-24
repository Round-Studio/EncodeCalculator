using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls.Items.EditItem;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class EditItem : UserControl
{
    private bool Chang = false;
    public ContentDialog ContentDialog;
    public UnitItem UnitItem;
    public string UUID;
    public EditItem(string uuid)
    {
        InitializeComponent();
        UnitItem = ItemMange.GetItem(uuid);
        if (this.UnitItem.IsMain)
        {
            NameBox.IsEnabled = false;
            DeleteItem.IsEnabled = false;
        }
        this.UUID = uuid;
        NameBox.Text = this.UnitItem.Name;
        ValueBox.Text = this.UnitItem.Value;
        NoteBox.Text = this.UnitItem.Note;
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
        Core.SetNowModifyTheStatus(true);
        ContentDialog.Hide();
    }

    private void DeepEditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ContentDialog.Hide();
        ContentDialog = new ContentDialog();
        
        var Show = new DeepEditItem(uuid:UUID);
        ContentDialog.Content = Show;
        ContentDialog.PrimaryButtonText = "取消";
        ContentDialog.Title = "高级编辑";
        ContentDialog.CloseButtonText = "确定";
        ContentDialog.DefaultButton = ContentDialogButton.Close;
        ContentDialog.CloseButtonClick += (_, _) =>
        {
            UnitItem.Value = ItemMange.ClassicValueToValue(Show.Value);
            UnitItem.ClassicValue = Show.InputBox.Text;
            
            var it = ItemMange.GetItemConfigClassByUUID(UnitItem.uuid);
            it.ClassicValue = UnitItem.ClassicValue;
            it.Value = UnitItem.Value;
        };
        ContentDialog.ShowAsync(Core.MainWindow);
    }

    private void NoteBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        
    }
}