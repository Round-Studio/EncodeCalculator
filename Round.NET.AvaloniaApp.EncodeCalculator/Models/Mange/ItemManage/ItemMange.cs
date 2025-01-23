using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

public class ItemMange
{
    public static List<RootConfig> Items = new List<RootConfig>();
    public static ListBox ItemListBox { get; set; }

    public class RootConfig
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
        public UnitItem Item { get; set; } = new UnitItem();
        public string UUID { get; set; }
        public int Type { get; set; } = 0;
        public bool IsMain { get; set; } = false;
    }

    public static void AddItem(RootConfig config)
    {
        if (!Deduplication.DeduplicationItem(config.Name))
        {
            var Guid = new Guid();
            config.UUID = Guid.NewGuid().ToString();
            config.Item.uuid = config.UUID;
            Items.Add(config);
            config.Item.NameBox.Content = config.Name;
            config.Item.IsMain = config.IsMain;
            config.Item.ValueBox.Text = config.Value;
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

    public static void ClearItems()
    {
        Items.Clear();
        ItemListBox.Items.Clear();
    }

    public static UnitItem GetItem(string uuid)
    {
        foreach (var item in Items)
        {
            if (item.UUID == uuid)
            {
                return item.Item;
            }
        }
        return null;
    }
    public static string GetValueForName(string name)
    {
        foreach (var it in Items)
        {
            if (it.Name == name)
            {
                return it.Value;
            }
        }
        return String.Empty;
    }
    public static string GetValueForUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.UUID == uuid)
            {
                return it.Value;
            }
        }
        return String.Empty;
    }

    public static void DeleteItemForUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.UUID == uuid)
            {
                ItemListBox.Items.Remove(it.Item);
                Items.Remove(it);
                break;
            }
        }
    }

    public static void SetNameForUUID(string uuid, string name)
    {
        foreach(var item in Items)
        {
            if (item.UUID == uuid)
            {
                item.Name = name;
            }
        }
    }

    public static string GetNameForUUID(string uuid)
    {
        foreach(var item in Items)
        {
            if (item.UUID == uuid)
            {
                return item.Name;
            }
        }
        return String.Empty;
    }
}