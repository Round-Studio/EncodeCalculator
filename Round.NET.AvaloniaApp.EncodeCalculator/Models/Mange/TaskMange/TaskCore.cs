using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Round.NET.AvaloniaApp.EncodeCalculator.Models.Mange.TaskMange;

public class TaskCore
{
    public class ActionConfig
    {
        public string uuid { get; set; } = string.Empty;
        public Action<string> action { get; set; }
    }
    public static List<ActionConfig> Actions = new();
    public static Thread TaskThread { get; set; } = null;

    public static void InitTaskCore()
    {
        if (TaskThread == null)
        {
            TaskThread = new Thread(() =>
            {
                while (true)
                {
                    foreach (var action in Actions)
                    {
                        Task.Run(()=>action.action(action.uuid));
                    }
                    Thread.Sleep(10);
                }
            });
            TaskThread.Start();
        }else if (TaskThread.IsAlive)
        {
            TaskThread.Abort();
        }
        else
        {
            TaskThread.Start();
        }
    }

    public static void RegisterAction(Action<string> action)
    {
        var uuid = Guid.NewGuid().ToString();
        Actions.Add(new ActionConfig { uuid = uuid, action = action });
    }
}