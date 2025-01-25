using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Round.NET.AvaloniaApp.EncodeCalculator.Models.Config;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Pages.SubPages;

public partial class SettingPage : UserControl
{
    public Config.RootConfig _config;

    public Config.RootConfig Config
    {
        get
        {
            GetConfig();
            return _config;
        }
        set
        {
            _config = value;
            RefreshControl();
        }
    }

    public void RefreshControl()
    {
        Err.IsChecked = _config.MessageModel[0];
        Wor.IsChecked = _config.MessageModel[1];
        Suc.IsChecked = _config.MessageModel[2];
        
        FontSizeBar.Value = _config.OutBoxFontSize;
        UpdateChannelAccelerationBox.IsChecked = _config.UpdateChannelAcceleration;
        AutoUpdateBox.IsChecked = _config.AutomaticUpdates;
    }

    public void GetConfig()
    {
        _config.MessageModel[0] = (bool)Err.IsChecked;
        _config.MessageModel[1] = (bool)Wor.IsChecked;
        _config.MessageModel[2] = (bool)Suc.IsChecked;
        
        _config.OutBoxFontSize = (int)FontSizeBar.Value;
        _config.UpdateChannelAcceleration = (bool)UpdateChannelAccelerationBox.IsChecked;
        _config.AutomaticUpdates = (bool)AutoUpdateBox.IsChecked;
    }
    
    public SettingPage()
    {
        InitializeComponent();
    }
}