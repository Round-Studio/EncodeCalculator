using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;
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

    private void ResetYourSettings_OnClick(object? sender, RoutedEventArgs e)
    {
        var sh = new ContentDialog()
        {
            Title = "还原设置",
            Content = "注意，这是高危操作！\n这将会删除 REC 的所有设置！",
            DefaultButton = ContentDialogButton.Close,
            PrimaryButtonText = "仍然继续",
            CloseButtonText = "取消"
        };
        sh.PrimaryButtonClick += (_, __) =>
        {
            Models.Config.Config.MainConfig = new Config.RootConfig();
            Models.Config.Config.SaveConfig();
            
            // 获取当前程序的路径
            string path = Assembly.GetEntryAssembly()!.Location.Replace(".dll", ".exe");
            // 启动新的进程
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
            Environment.Exit(0);
        };
        sh.ShowAsync(Core.MainWindow);
    }
}