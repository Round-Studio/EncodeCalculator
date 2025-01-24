using System;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls.Items.EditItem;

public partial class DeepEditItem : UserControl
{
    private string _value;
    private bool _isEditing = false;
    
    public UnitItem ThisUnitItem { get; set; }

    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            if (_isEditing)
            {
                InputBox.Text = value;
            }
        }
    }

    public DeepEditItem(string uuid)
    {
        InitializeComponent();
        ThisUnitItem = ItemMange.GetItem(uuid);

        NameBox.Content = NameBox.Content.ToString().Replace("{name}",ThisUnitItem.Name);
        Value = ThisUnitItem.ClassicValue;
        InputBox.Text = Value;
        _isEditing = true;

        foreach (var item in ItemMange.Items)
        {
            if (!item.IsMain)
            {
                var aditem = new ComboBoxItem()
                {
                    Content = $"{item.Name}()"
                };
                if(item.Name == ThisUnitItem.Name) aditem.IsEnabled = false;
                FuncBox.Items.Add(aditem);
            }
        }
    }

    private void InputBox_OnTextChanged(object? sender, EventArgs e)
    {
        if (_isEditing)
        {
            _isEditing = false;
            _value = InputBox.Text;   
            _isEditing = true;
        }
    }

    private void AddFuncButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var item = FuncBox.SelectionBoxItem.ToString();
        var bo = item != "(空)";
        if(bo) Value += item;
    }
}