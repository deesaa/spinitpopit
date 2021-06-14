using System;
using System.Collections.Generic;
using Client.States;
using UnityEngine;

namespace JDS
{
    /// <summary>
    /// Window Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class WM<T>
    {
        private static Dictionary<T, IWindow> windows
            = new Dictionary<T, IWindow>();

        public static void RegisterWindow(T windowType, IWindow window)
        {
            windows[windowType] = window;
        }

        public static void Show(T windowType)
        {
            if (windows.ContainsKey(windowType))
            {
                windows[windowType].Show();
            }
            else
                DebugLog.Log($"WindowName {windowType} does not registered", "WindowsManager");
        }
        
        public static void Hide(T windowType)
        {
            if(windows.ContainsKey(windowType))
                windows[windowType].Hide();
            else
                DebugLog.Log($"WindowName {windowType} does not registered", "WindowsManager");
        }

        public static void HideAll()
        {
            foreach (var window in windows)
            {
                window.Value.Hide();
            }
        }
    }
}