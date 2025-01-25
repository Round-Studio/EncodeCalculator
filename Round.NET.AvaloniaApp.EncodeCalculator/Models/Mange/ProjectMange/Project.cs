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
    
    public class FuncItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ClassicValue { get; set; }
        public string Note { get; set; } = "无备注";
        public bool IsMain { get; set; } = false;
    }
    public class CompItem
    {
        public string Name { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string CompareTypes { get; set; }
        public string Note { get; set; } = "无备注";
    }
    public class ItemListClass
    {
        public FuncItem Item { get; set; } = new FuncItem();
        public CompItem CompItem { get; set; } = new CompItem();
        public Type.Type.NodeType Type { get; set; } = Models.Type.Type.NodeType.Function;
    }
    public class SaveProject
    {
        public static string GetTheContentsOfTheSaveFile()
        {
            string result = Regex.Unescape(JsonSerializer.Serialize(GetNowRoot(), new JsonSerializerOptions() { WriteIndented = true })); //获取结果并转换成正确的格式
            
            return result;
        }

        public static Root GetNowRoot()
        {
            var root = new Root();
            foreach (var item in ItemMange.Items)
            {
                if (item.Type == Models.Type.Type.NodeType.Function)
                {
                    root.Items.Add(new ItemListClass()
                    {
                        Item = new FuncItem()
                        {
                            Name = item.FuncItem.Name,
                            Value = item.FuncItem.Value.Replace("\n","").Replace("\r",""),
                            ClassicValue = item.FuncItem.ClassicValue.Replace("\n","\\n").Replace("\r","\\r"),
                            IsMain = item.FuncItem.IsMain,
                            Note = item.FuncItem.Note
                        },
                    
                        Type = item.Type   
                    });   
                }
                else
                {
                    root.Items.Add(new ItemListClass()
                    {
                        CompItem = new CompItem()
                        {
                            Name = item.CompItem.Name,
                            Value1 = item.CompItem.Value1,
                            Value2 = item.CompItem.Value2,
                            Note = item.CompItem.Note
                        },
                    
                        Type = item.Type   
                    });
                }
            }
            return root;
        }
    }
    public class OpenProject
    {
        public static void OpenProjectFile(string path)
        {
            var FileContents = File.ReadAllText(path);
            var Roots = JsonSerializer.Deserialize<Root>(FileContents);
            
            OpenProjectCore(Roots);
        }

        public static void OpenProjectCore(Root root)
        {
            ItemMange.ClearItems();

            foreach (var item in root.Items)
            {
                if (item.Type == Models.Type.Type.NodeType.Function)
                {
                    ItemMange.AddFuncItem(new ItemMange.FuncItemConfig()
                    {
                        Name = item.Item.Name,
                        Value = item.Item.Value,
                        ClassicValue = item.Item.ClassicValue,
                        IsMain = item.Item.IsMain,
                        Note = item.Item.Note
                    });
                }
                else if (item.Type == Models.Type.Type.NodeType.Comparison)
                {
                    ItemMange.AddCompItem(new ItemMange.CompItemConfig()
                    {
                        Name = item.CompItem.Name,
                        Value1 = item.CompItem.Value1,
                        Value2 = item.CompItem.Value2,
                        Note = item.CompItem.Note
                    });
                }
            }
        }
    }

    public class NewProject
    {
        public static void NewProjectCore()
        {
            Edit.Edit.EditMode = true;
            ItemMange.ClearItems();
            ItemMange.AddFuncItem(new ItemMange.FuncItemConfig()
            {
                Value = "1+1",
                Name = "Main",
                ClassicValue = "1+1",
                IsMain = true,
                Note = "程序计算主入口"
            });
            
            Core.ProjectPath = String.Empty;
            Core.ProjectName = $"无标题.{FILE_TYPE}";
            Core.SetNowModifyTheStatus(false);
            Edit.Edit.ClearEdits();
            Edit.Edit.EditMode = false;
        }
    }
}