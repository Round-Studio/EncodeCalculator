using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Downloader;
using FluentAvalonia.UI.Controls;
using Newtonsoft.Json.Linq;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Update
{
    public class VersionConfig
    {
        public string Version { get; set; }
        public string Time { get; set; }
        public string HtmlUrl { get; set; }
        public List<AssetConfig> Assets { get; set; } = new List<AssetConfig>();
    }

    public class AssetConfig
    {
        public string URL { get; set; }
        public string Name { get; set; }
    }

    public class BackUpdate
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly string repoUrl =
            "https://api.github.com/repos/Round-Studio/Round.NET.AvaloniaApp.EncodeCalculator/releases?per_page=100";

        public static List<VersionConfig> VersionConfigs { get; set; } = new List<VersionConfig>();

        public static async Task RefreshUpdateConfig()
        {
            VersionConfigs.Clear();
            try
            {
                // 添加User-Agent头
                client.DefaultRequestHeaders.Add("User-Agent", "REC-Web-UpdateService");

                HttpResponseMessage response = await client.GetAsync(repoUrl);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed to fetch data from GitHub API. Status Code: " + response.StatusCode);
                    return;
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                JArray releases = JArray.Parse(responseBody);

                foreach (JObject release in releases)
                {
                    string version = release["name"]?.ToString();

                    VersionConfig versionConfig = new VersionConfig
                    {
                        Version = version,
                        HtmlUrl = release["html_url"]?.ToString(),
                        Time = release["published_at"]?.ToString(),
                    };

                    JArray assets = release["assets"] as JArray;
                    if (assets != null)
                    {
                        foreach (JObject asset in assets)
                        {
                            string assetUrl = asset["browser_download_url"]?.ToString();
                            if (!string.IsNullOrEmpty(assetUrl))
                            {
                                var url = "";
                                if (Config.Config.MainConfig.UpdateChannelAcceleration)
                                {
                                    url = $"https://gh.api.99988866.xyz/{assetUrl}";
                                }
                                else
                                {
                                    url = $"{assetUrl}";
                                }

                                versionConfig.Assets.Add(new AssetConfig()
                                {
                                    URL = url,
                                    Name = asset["name"]?.ToString()
                                });
                                // Console.WriteLine($"  名字: {asset["name"]?.ToString()}");
                                // Console.WriteLine($"  资源: {url}\n");
                            }
                        }
                    }

                    VersionConfigs.Add(versionConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching or parsing the data: " + ex.Message);
            }
        }

        public static async Task DownloadFileAsync(string url, string fileName, string ver, ProgressBar bar, ContentDialog shd, Label jd)
        {
            try
            {
                Console.WriteLine("Downloading file: " + fileName);
                Console.WriteLine($"Downloading ver: {ver}");
                Console.WriteLine($"Downloading url: {url}");

                var downloader = new DownloadService();
                downloader.DownloadProgressChanged += (sender, args) =>
                {
                    Console.WriteLine($"下载进度: {args.ProgressPercentage}%");
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        bar.Value = args.ProgressPercentage;
                        jd.Content = $"进度：{args.ProgressPercentage}%";
                    });
                };

                // 确保文件路径有效
                Directory.CreateDirectory(Update.UpdateDirPath);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{Update.UpdateDirPath}\\{ver.Replace(".", "")}{fileName}");
                Console.WriteLine($"开始下载文件到: {filePath}");

                //await downloader.DownloadFileTaskAsync($"https://gh.api.99988866.xyz/{url}", filePath);
                await downloader.DownloadFileTaskAsync($"{url}", filePath);

                Console.WriteLine($"文件已成功下载到: {filePath}");

                // 检查文件是否存在
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"文件未找到: {filePath}");
                }

                Process.Start(filePath);

                Task.Run(() =>
                {
                    Thread.Sleep(800);
                    Environment.Exit(0);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"下载失败: {ex.Message}");
                shd.Title = "下载失败";
                shd.Content = $"下载失败: {ex.Message}";
                shd.CloseButtonText = "确定";
                shd.ShowAsync(Core.MainWindow);
            }
        }
    }
}