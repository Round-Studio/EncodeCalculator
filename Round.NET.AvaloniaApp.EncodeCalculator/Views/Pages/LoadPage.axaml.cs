using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Round.NET.AvaloniaApp.EncodeCalculator.Models;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Views.Pages;

public partial class LoadPage : UserControl
{
    public LoadPage()
    {
        InitializeComponent();

        Task.Run(() =>
        {
            Thread.Sleep(100);
            string url = "https://gitee.com/minecraftyjq/round.-studio.-config/raw/master/Config/Config.json";

            try
            {
                // 使用HttpClient获取JSON文件内容
                using HttpClient client = new HttpClient();
                string jsonString = client.GetStringAsync(url).Result;
                
                Console.WriteLine(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            
            Dispatcher.UIThread.Invoke(() =>
            {
                Core.MainWindow.Content = new MainView();
            });
        });
    }
}