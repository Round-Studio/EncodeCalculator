using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class BackUpdatePage : UserControl
{
    public BackUpdatePage(string name,string url,string ver)
    {
        InitializeComponent();
        Func(name,url,ver);
    }

    private async Task Func(string name,string url,string ver)
    {
        await Models.Update.BackUpdate.DownloadFileAsync(url, name, ver, ProgressBarBox, (ContentDialog)this.Parent, jd);
    }
}