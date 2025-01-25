using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Downloader;
using FluentAvalonia.UI.Controls;
using Newtonsoft.Json.Linq;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Update;

public class Update
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string repoUrl = "https://api.github.com/repos/Round-Studio/Round.NET.AvaloniaApp.EncodeCalculator/releases/latest";
    private static JObject releaseInfo;
    public static string GetCurrentVersion()
    {
        // 获取当前程序的版本号
        Assembly assembly = Assembly.GetExecutingAssembly();
        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
        return fvi.ProductVersion;
    }

    public static string GetNewVersion()
    {
        return releaseInfo["name"].ToString();
    }
    public static string GetNewVersionTime()
    {
        return releaseInfo["published_at"].ToString();
    }
    public static bool GetUpdate()
    {
        try
        {
            // 添加User-Agent头
            client.DefaultRequestHeaders.Add("User-Agent", "YourApp-Name");

            // 获取当前程序的版本号
            string currentVersion = GetCurrentVersion();
            Console.WriteLine($"当前版本: {currentVersion}");

            // 获取最新版本信息
            string jsonResponse = client.GetStringAsync(repoUrl).Result; 
            releaseInfo = JObject.Parse(jsonResponse);

            string latestVersion = releaseInfo["name"].ToString();
            Console.WriteLine($"最新版本: {latestVersion}");

            // 检查是否为当前版本
            if (latestVersion == currentVersion)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    
    public static async Task UpdateCore(ProgressBar bar,ContentDialog shd)
    {
        // 获取系统架构
        string architecture = RuntimeInformation.ProcessArchitecture.ToString().ToLower();
        string installerName = GetInstallerName(architecture);

        if (string.IsNullOrEmpty(installerName))
        {
            Console.WriteLine("未找到适合当前系统架构的安装程序。");
            return;
        }

        // 查找对应的安装文件
        JArray assets = releaseInfo["assets"] as JArray;
        JObject installerAsset = assets?.FirstOrDefault(asset => asset["name"].ToString() == installerName) as JObject;

        if (installerAsset != null)
        {
            string downloadUrl = installerAsset["browser_download_url"].ToString();
            Console.WriteLine($"正在下载: {installerName}"); 
            await DownloadFileAsync(downloadUrl, installerName,bar,shd);
        }
        else
        {
            Console.WriteLine("未找到对应的安装文件。");
        }
    }

    private static string GetInstallerName(string architecture)
    {
        switch (architecture)
        {
            case "arm64":
                return "Round.NET.AvaloniaApp.EncodeCalculator.Desktop.win.arm64.installer.exe";
            case "x64":
                return "Round.NET.AvaloniaApp.EncodeCalculator.Desktop.win.x64.installer.exe";
            case "x86":
                return "Round.NET.AvaloniaApp.EncodeCalculator.Desktop.win.x86.installer.exe";
            default:
                return null;
        }
    }

    private static async Task DownloadFileAsync(string url, string fileName,ProgressBar bar,ContentDialog shd)
    {
        try
        {
            var downloadOpt = new DownloadConfiguration()
            {
                BufferBlockSize = 10240,
                ChunkCount = 256,
                MaxTryAgainOnFailover = 5,
                ParallelDownload = true,
                ParallelCount = 256,
                Timeout = 1000,
                ClearPackageOnCompletionWithFailure = true,
                MinimumSizeOfChunking = 64,
                ReserveStorageSpaceBeforeStartingDownload = true
            };

            var downloader = new DownloadService(downloadOpt);
            downloader.DownloadProgressChanged += (sender, args) =>
            {
                // 确保事件被触发
                Console.WriteLine($"下载进度: {args.ProgressPercentage}%");
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    bar.Value = args.ProgressPercentage;
                });
            };

            // 确保文件路径有效
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            Console.WriteLine($"开始下载文件到: {filePath}");

            await downloader.DownloadFileTaskAsync(url, filePath);
            Console.WriteLine($"文件已成功下载到: {filePath}");

            Process.Start(filePath);
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                Environment.Exit(0);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"下载失败: {ex.Message}");
            shd.Hide();
        }
    }
}