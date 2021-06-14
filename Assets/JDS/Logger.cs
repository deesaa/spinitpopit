using UnityEngine;

namespace JDS
{
    public static class DebugLog
    {
        public static LogOption LogOption = LogOption.None;
        public static void Log(string message, string name = "", Object o = null)
        {
#if UNITY_EDITOR
            Debug.LogFormat(LogType.Log, LogOption, o, $"{name} : <color=cyan>{message}</color>");
#endif
        }
        
        public static void LogWarning(string message, string name = "", Object o = null)
        {
#if UNITY_EDITOR
            Debug.LogFormat(LogType.Warning, LogOption, o, $"{name} : <color=cyan>{message}</color>");
#endif
        }

        public static void LogError(string message, string name = "", Object o = null)
        {
#if UNITY_EDITOR
            Debug.LogFormat(LogType.Error, LogOption, o, $"{name} : <color=cyan>{message}</color>");
#endif
        }
    }
}