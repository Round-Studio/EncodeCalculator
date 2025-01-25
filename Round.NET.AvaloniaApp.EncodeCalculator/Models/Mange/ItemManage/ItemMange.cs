using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls.Items.ItemControls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

public class ItemMange
{
    public static List<RootConfig> Items = new List<RootConfig>();
    public static ListBox ItemListBox { get; set; }
    public class RootConfig
    {
        public FuncItemConfig FuncItem { get; set; } = new FuncItemConfig();
        public CompItemConfig CompItem { get; set; } = new CompItemConfig();
        public Type.Type.NodeType Type { get; set; } = Models.Type.Type.NodeType.Function;
    }

    public class FuncItemConfig
    {
        public string Value
        {
            get
            {
                return Item.ValueBox.Text;
            }
            set
            {
                Item.ValueBox.Text = value;
            }
        }
        public string ClassicValue { get; set; } = string.Empty;
        public string Name
        {
            get
            {
                return Item.Name;
            }
            set
            {
                Item.NameBox.Content = value;
                Item.Name = value;
            }
        }
        public string Note
        {
            get
            {
                return Item.Note;
            }
            set
            {
                Item.Note = value;
            }
        }
        public UnitItem Item { get; set; } = new UnitItem();
        public bool IsMain { get; set; } = false;
        public string UUID { get; set; }
    }
    
    public class CompItemConfig
    {
        public string Value1
        {
            get
            {
                return Item.Value1;
            }
            set
            {
                Item.Value1 = value;
            }
        }
        public string Value2 { 
            get
            {
                return Item.Value2;
            }
            set
            {
                Item.Value2 = value;
            } 
        }
        public string Name
        {
            get
            {
                return Item.NameBox.Content.ToString();
            }
            set
            {
                Item.NameBox.Content = value;
            }
        }
        public string Note
        {
            get
            {
                return Item.Note;
            }
            set
            {
                Item.Note = value;
            }
        }
        public string UUID { get; set; }
        public ComparisonItem Item { get; set; } = new ComparisonItem();
        public Type.Type.CompareTypes CompareType = Type.Type.CompareTypes.Equals;
    }
    public static void AddFuncItem(FuncItemConfig config)
    {
        if (!Deduplication.DeduplicationItem(config.Name))
        {
            var Guid = new Guid();
            config.UUID = Guid.NewGuid().ToString();
            config.Item.uuid = config.UUID;
            Items.Add(new RootConfig()
            {
                FuncItem = config,
                Type = Models.Type.Type.NodeType.Function
            });
            config.Item.NameBox.Content = config.Name;
            config.Item.IsMain = config.IsMain;
            config.Item.ValueBox.Text = config.Value;
            config.Item.ClassicValue = config.ClassicValue;
            config.Item.Note = config.Note;
            ItemListBox.Items.Add(config.Item);
        }
        else
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                var Show = new ContentDialog();
                Show.Title = "重名错误";
                Show.Content = $"发生重名错误！\n错误对象：{config.Name}\n对象值    ：{config.Value}";
                Show.CloseButtonText = "确定";
                Show.DefaultButton = ContentDialogButton.Close;
                Show.ShowAsync(Core.MainWindow);
            });
        }
    }
    public static void AddCompItem(CompItemConfig compItemConfig) // 未完成!!!!!!!!!!!!!!!!!!!!!!!
    {
        var Guid = new Guid();
        compItemConfig.UUID = Guid.NewGuid().ToString();
        compItemConfig.Item = new ComparisonItem();
        compItemConfig.Item.uuid = compItemConfig.UUID;
        compItemConfig.Item.Name = compItemConfig.Name;
        compItemConfig.Item.Note = compItemConfig.Note;
        compItemConfig.Item.Value1 = compItemConfig.Value1;
        compItemConfig.Item.Value2 = compItemConfig.Value2;
        Items.Add(new RootConfig()
        {
            CompItem = compItemConfig,
            Type = Models.Type.Type.NodeType.Comparison
        });
        
        ItemListBox.Items.Add(compItemConfig.Item);
    }

    public static void ClearItems()
    {
        Items.Clear();
        ItemListBox.Items.Clear();
    }

    public static UnitItem GetItem(string uuid)
    {
        foreach (var item in Items)
        {
            if (item.FuncItem.UUID == uuid)
            {
                return item.FuncItem.Item;
            }
        }
        return null;
    }
    public static string GetValueForName(string name)
    {
        foreach (var it in Items)
        {
            if (it.FuncItem.Name == name)
            {
                return it.FuncItem.Value;
            }
        }
        return String.Empty;
    }
    public static string GetValueForUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.FuncItem.UUID == uuid)
            {
                return it.FuncItem.Value;
            }
        }
        return String.Empty;
    }

    public static void DeleteItemForUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.FuncItem.UUID == uuid)
            {
                ItemListBox.Items.Remove(it.FuncItem.Item);
                Items.Remove(it);
                break;
            }
        }
    }

    public static void SetNameForUUID(string uuid, string name)
    {
        foreach(var item in Items)
        {
            if (item.FuncItem.UUID == uuid)
            {
                item.FuncItem.Name = name;
            }
        }
    }

    public static string GetNameForUUID(string uuid)
    {
        foreach(var item in Items)
        {
            if (item.FuncItem.UUID == uuid)
            {
                return item.FuncItem.Name;
            }
        }
        return String.Empty;
    }

    public static RootConfig GetItemConfigClassByUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.FuncItem.UUID == uuid)
            {
                return it;
            }
        }
        return null;
    }
    public static string ClassicValueToValue(string classicvalue)
    {
        return classicvalue.Replace("\n", "").Replace("\r", "");
    }
}