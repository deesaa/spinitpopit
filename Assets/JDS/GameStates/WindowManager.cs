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
    public static class WM<T> where T : System.Enum
    {
        private static Dictionary<T, IWindow> windows
            = new Dictionary<T, IWindow>();

        public static void RegisterWindow(T windowType, IWindow window)
        {
            windows[windowType] = window;
        }

        public static void ShowWindow(T windowType)
        {
            if (windows.ContainsKey(windowType))
            {
                windows[windowType].Show();
            }
            
#if UNITY_EDITOR
            else
                Debug.Log($"WindowsManager: WindowName {windowType} does not registered");
#endif
        }
        
        public static void HideWindow(T windowType)
        {
            if(windows.ContainsKey(windowType))
                windows[windowType].Hide();

#if UNITY_EDITOR
            else
                Debug.Log($"WindowsManager: WindowName {windowType} does not registered");
#endif
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