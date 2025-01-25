using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

public partial class BackUpdate : UserControl
{
    public BackUpdate()
    {
        InitializeComponent();

        System.Threading.Tasks.Task.Run(async () =>
        {
            await Models.Update.BackUpdate.RefreshUpdateConfig();
            Dispatcher.UIThread.Invoke(() =>
            {
                LoadBox.Children.Clear();
                foreach (var item in Models.Update.BackUpdate.VersionConfigs)
                {
                    var bu = new Button()
                    {
                        VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                        Content = "下载",
                        Margin = new Thickness(10),
                    };
                    var names = "";
                    var url = "";
                    foreach (var ass in item.Assets)
                    {
                        if (ass.Name ==
                            Models.Update.Update.GetInstallerName(RuntimeInformation.ProcessArchitecture.ToString()
                                .ToLower()))
                        {
                            names = ass.Name;
                            url = ass.URL;
                        }
                    }
                    bu.Click += async (s, e) =>
                    {
                        ((ContentDialog)this.Parent).Hide();

                        var show = new ContentDialog();
                        show.Title = "回滚更新...";
                        show.Content = new BackUpdatePage(names,url,item.Version);
                        show.ShowAsync(Core.MainWindow);
                    };
                    LoadBox.Children.Add(new DockPanel()
                    {
                        Children =
                        {
                            new Label()
                            {
                                Content = item.Version,
                                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                                Margin = new Thickness(5),
                            },
                            new HyperlinkButton()
                            {
                                NavigateUri = new Uri(item.HtmlUrl),
                                Content = "前往网页查看",
                                Margin = new Thickness(5),
                            },
                            bu
                        }
                    });
                }
            });
        });
    }
}