using System;
using System.Collections.Generic;
using MonoGameLibrary;
using WizardsVsWirebacks.Scenes.City;

namespace WizardsVsWirebacks;

public static class DebugLogger
{
    public static bool IsEnabled { get; set; } = true;
    
    private static readonly Queue<(string message, DateTime timestamp)> _messageQueue = new();
    private static readonly TimeSpan _defaultDelay = TimeSpan.FromMilliseconds(2000); // Adjust as needed
    
    private static DateTime _lastLogTime = DateTime.Now;
    
    private const float _printDelay = 3000f;
    private static float _pdCounter = 0;
    public static void Log(string message)
    {
        //Console.Out.WriteLine(_pdCounter.ToString() + _lastLogTime.ToString() + DateTime.Now.ToString());
        if (DateTime.Now - _lastLogTime > _defaultDelay)
        {
            _messageQueue.Enqueue((message, DateTime.Now));

            _lastLogTime = DateTime.Now;
        }
    }

    public static void WriteLogs()
    {
        if (_pdCounter > _printDelay)
        {
            var (message, timestamp) = _messageQueue.Dequeue();
            Console.WriteLine($"[{timestamp:HH:mm:ss.fff}] {message}");
            _pdCounter %= _printDelay;
        }
        else
        {
            _pdCounter += Core.DT * 1000f;
        }
    }

    public static void Clear()
    {
        _messageQueue.Clear();
    }
}