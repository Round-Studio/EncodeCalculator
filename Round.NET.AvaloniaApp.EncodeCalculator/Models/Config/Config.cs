using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Config;

public class Config
{
    public const string ConfigFilePath = "../REC.Config/Config.json";
    public static RootConfig MainConfig { get; set; } = new RootConfig();
    public class RootConfig
    {
        public int OutBoxFontSize { get; set; } = 20;
        public List<bool> MessageModel { get; set; } = new List<bool>()
        {
            true,
            true,
            true
        };
        public bool UpdateChannelAcceleration { get; set; } = true;
        public bool AutomaticUpdates { get; set; } = true;
    }

    public static void LoadConfig()
    {
        if (!File.Exists(ConfigFilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));
            SaveConfig();
        }
        var json = File.ReadAllText(ConfigFilePath);
        MainConfig = JsonSerializer.Deserialize<RootConfig>(json);
    }

    public static void SaveConfig()
    {
        string result = Regex.Unescape(JsonSerializer.Serialize(MainConfig, new JsonSerializerOptions() { WriteIndented = true })); //获取结果并转换成正确的格式
        File.WriteAllText(ConfigFilePath, result);
    }
}