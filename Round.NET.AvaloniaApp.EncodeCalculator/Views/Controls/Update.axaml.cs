using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class Update : UserControl
{
    public Update()
    {
        InitializeComponent();
        
        VersionBox.Content = VersionBox.Content.ToString().Replace("{UpdateVersion}",Models.Update.Update.GetNewVersion());
        TimeBox.Content = TimeBox.Content.ToString().Replace("{UpdateTime}",Models.Update.Update.GetNewVersionTime());
        ThisVersionBox.Content = ThisVersionBox.Content.ToString().Replace("{Version}",Models.Update.Update.GetCurrentVersion());
        Models.Update.Update.UpdateCore(ProgressBarBox,(ContentDialog)this.Parent,jd);  
    }
}