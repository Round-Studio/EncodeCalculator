using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using HeroIconsAvalonia.Enums;
using ReactiveUI;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class UnitItem : UserControl
{
    public string uuid { get; set; }
    public string _value;
    public string _note = "无备注";

    public string Name
    {
        get
        {
            return NameBox.Content.ToString();
        }
        set
        {
            NameBox.Content = value;
        }
    }
    public string Note
    {
        get
        {
            return _note;
        }
        set
        {
            _note = value;
            NoteBox.Content = value;
        }
    }
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            ValueBox.Text = value.Replace("\n", "");
        }
    }
    public string ClassicValue { get; set; }
    
    public bool IsMain {
        get
        {
            return Name == "Main";
        }
        set
        {
            if(value == true)
            {
                IconBox.Type = IconType.RocketLaunch;
            }
        }
    }
    public UnitItem()
    {
        InitializeComponent();
    }

    private void MoreButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            var Show = new ContentDialog();
            var Edit = new EditItem(uuid);
            Edit.ContentDialog = Show;
            
            Show.Title = "编辑项";
            Show.CloseButtonText = "确定";
            Show.PrimaryButtonText = "取消";
            Show.Content = Edit;
            Show.DefaultButton = ContentDialogButton.Close;
            Show.CloseButtonClick += (_, __) =>
            {
                ValueBox.Text = Edit.ValueBox.Text;
                NameBox.Content = Edit.NameBox.Text;
                Note = Edit.NoteBox.Text;
                
                Core.SetNowModifyTheStatus(true);
            };
            Show.ShowAsync(Core.MainWindow);
        });
    }

    private void ValueBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        Value = ValueBox.Text;
        Core.SetNowModifyTheStatus(true);
    }
}