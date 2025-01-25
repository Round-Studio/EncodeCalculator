using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Type;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls.Items.EditItem.CompItems;

public partial class AddCompItem : UserControl
{
    public Type.CompareTypes CompareType { get; set; } 
    public AddCompItem()
    {
        InitializeComponent();
    }
}