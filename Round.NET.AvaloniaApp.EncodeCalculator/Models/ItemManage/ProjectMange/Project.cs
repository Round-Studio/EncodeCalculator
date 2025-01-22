using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Avalonia.Threading;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage.ProjectMange;

public class Project
{
    public const string FILE_TYPE = "ejson";
    public const string FILE_EXTENSION = $".{FILE_TYPE}";
    public const string DEFAULT_FILE_NAME = $"无标题.{FILE_TYPE}";
    
    public class Root
    {
        public string Time { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public List<ItemListClass> Items { get; set; } = new List<ItemListClass>();
    }
    public class ItemListClass
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsMain { get; set; } = false;
    }
    public class SaveProject
    {
        public static string GetTheContentsOfTheSaveFile()
        {
            var root = new Root();
            foreach (var item in ItemMange.Items)
            {
                root.Items.Add(new ItemListClass()
                {
                    Name = item.Name,
                    Value = item.Value,
                    IsMain = item.IsMain
                });
            }
            
            string result = Regex.Unescape(JsonSerializer.Serialize(root, new JsonSerializerOptions() { WriteIndented = true })); //获取结果并转换成正确的格式
            
            return result;
        }
    }
    public class OpenProject
    {
        public static void OpenProjectFile(string path)
        {
            var FileContents = File.ReadAllText(path);
            var Root = JsonSerializer.Deserialize<Root>(FileContents);
            ItemMange.ClearItems();

            foreach (var item in Root.Items)
            {
                ItemMange.AddItem(new ItemMange.RootConfig()
                {
                    Name = item.Name,
                    Value = item.Value,
                    IsMain = item.IsMain
                });
            }
        }
    }

    public class NewProject
    {
        public static void NewProjectCore()
        {
            ItemMange.ClearItems();
            ItemMange.AddItem(new ItemMange.RootConfig()
            {
                Value = "1+1",
                Name = "Main",
                IsMain = true,
            });
            
            Core.ProjectPath = String.Empty;
            Core.ProjectName = $"无标题.{FILE_TYPE}";
            Core.ModifyTheStatus = false;
        }
    }
}