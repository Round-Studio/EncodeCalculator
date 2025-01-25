using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls.Items.ItemControls;

public partial class ComparisonItem : UserControl
{public string uuid { get; set; }
    public string _value1 = string.Empty;
    public string _value2 = string.Empty;
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
    public string Value1
    {
        get
        {
            return _value1;
        }
        set
        {
            _value1 = value;
            ValueBox1.Text = value;
        }
    }
    public string Value2
    {
        get
        {
            return _value2;
        }
        set
        {
            _value2 = value;
            ValueBox2.Text = value;
        }
    }
    public ComparisonItem()
    {
        InitializeComponent();
    }

    private void ValueBox_OnTextChanging(object? sender, TextChangingEventArgs e)
    {
        try
        {
            Value1 = ValueBox1.Text;
            Value2 = ValueBox2.Text;
        }catch{}
    }
}