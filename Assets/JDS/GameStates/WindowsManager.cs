using System;
using System.Collections.Generic;
using Client.States;
using UnityEngine;

namespace JDS
{
    public static class WindowsManager<T> where T : System.Enum
    {
        private static Dictionary<T, IWindow> windows
            = new Dictionary<T, IWindow>();

        public static void RegisterWindow(T windowType, IWindow window)
        {
            windows[windowType] = window;
        }

        public static void ShowWindow(T windowType)
        {
            if(windows.ContainsKey(windowType))
                windows[windowType].Show();
            else
                Debug.Log($"WindowsManager: WindowName {windowType} does not registered");
        }
        
        public static void HideWindow(T windowType)
        {
            if(windows.ContainsKey(windowType))
                windows[windowType].Hide();
            else
                Debug.Log($"WindowsManager: WindowName {windowType} does not registered");
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