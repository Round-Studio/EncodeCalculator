using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using Round.NET.AvaloniaApp.EncodeCalculator.Views.Controls;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.ItemManage;

public class ItemMange
{
    public static List<UnitItem> Items = new List<UnitItem>();
    public static ListBox ItemListBox { get; set; }

    public class RootConfig
    {
        public string Value { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UUID { get; set; } = string.Empty;
        public int Type { get; set; } = 0;
        public bool IsMain { get; set; } = false;
    }

    public static void AddItem(RootConfig config)
    {
        if (!Deduplication.DeduplicationItem(config.Name))
        {
            var Guid = new Guid();
            var it = new UnitItem(config);
            it.Config.UUID = Guid.NewGuid().ToString();
            Items.Add(it);
            ItemListBox.Items.Add(it);
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

    public static string GetValueForName(string name)
    {
        foreach (var it in Items)
        {
            if (it.NameBox.Content.ToString() == name)
            {
                return it.ValueBox.Text.ToString();
            }
        }
        return String.Empty;
    }
    public static string GetValueForUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.Config.UUID == uuid)
            {
                return it.ValueBox.Text.ToString();
            }
        }
        return String.Empty;
    }

    public static void DeleteItemForUUID(string uuid)
    {
        foreach (var it in Items)
        {
            if (it.Config.UUID == uuid)
            {
                ItemListBox.Items.Remove(it);
                Items.Remove(it);
                break;
            }
        }
    }
}