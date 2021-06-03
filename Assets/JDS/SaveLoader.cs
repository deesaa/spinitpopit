using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JDS
{
    public static class SaverLoader
    {
        private static readonly List<SaveLoad> Registered = new List<SaveLoad>();

        /// <summary>
        /// Loads (by Type and gameObject as persistence ID) or creates (By Type, constructor and ISaveLoad->SetDefault())
        /// instance of T : ISaveLoad and registers it for saving
        /// </summary>
        public static T Load<T>(GameObject gameObject) where T : SaveLoad, new()
        {
            //Vector3 v = gameObject.transform.position;
            //int uHashCode = $"{v.x}{v.y}{v.z}{gameObject.name}".GetHashCode();
            
            int uHashCode = gameObject.transform.GetFullName().GetHashCode();
            
            T @new;
            
            if (PlayerPrefs.HasKey($"{typeof(T)}_{uHashCode}"))
            {
                string json = PlayerPrefs.GetString($"{typeof(T)}_{uHashCode}");
                @new = JsonUtility.FromJson<T>(json);
                return Register(@new, uHashCode);
            }
            
            @new = new T();
            @new.SetDefault();
            return Register(@new, uHashCode);
        }

        private static void Save<T>(T data) where T : SaveLoad
        {
            if (data.DeleteFlag)
            {
                PlayerPrefs.DeleteKey($"{data.GetType()}_{data.UHashCode}");
            }
            else
            {
                string json = JsonUtility.ToJson(data);
                PlayerPrefs.SetString($"{data.GetType()}_{data.UHashCode}", json);
            }
        }
    
        private static T Register<T>(T data, int persistenceHash) where T : SaveLoad
        {
            data.UHashCode = persistenceHash;
            Registered.Add(data);
            return data;
        }

        public static void DeleteAll() => PlayerPrefs.DeleteAll();
        public static void SaveAll() => Registered.ForEach(Save);
    }
}
