using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    public class GlobalPool : MonoBehaviour
    {
        public static GlobalPool Instance { get; private set; }

        public Transform pooledObjectsFolder;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        [SerializeField]
        private List<IPoolable> prototypes;
    
        private readonly Dictionary<Type, List<IPoolable>> _poolDictionary 
            = new Dictionary<Type, List<IPoolable>>();

        private IPoolable Create<T>()
        {
            var element = prototypes.Find(x => x.GetType() == typeof(T));
            if(element == null) 
            {
                Debug.Log($"No prototype for this type {typeof(T)}");
                return null;    
            }
        
            var newElement = Instantiate(element);
            newElement.gameObject.SetActive(false);

            if (_poolDictionary.TryGetValue(typeof(T), out List<IPoolable> elementsPool))
                elementsPool.Add(newElement);
            else
                _poolDictionary.Add(typeof(T), new List<IPoolable>() { newElement });

            newElement.parent = this;
        
            return newElement;
        }
        
        public T Take<T>() where T : IPoolable
        {
            // Ищем в библиотеке лист по типу, хранящий запуленные объекты этого же типа.
            // Если лист найден, проверяем в нем наличие нужного объекта типа:
            // если его нет, то создаем новый объект этого типа.
            // Если библиотека по типу не найдена, то создаем новый объект этого типа.
            // При создании объекста типа, бибтотека типа создастся сама. 
            // Активируем найденный/созданный объект, устанавливаем родительский пул - этот,
            // удаляем из листа библиотеки, так как объект "отдан" из пула.
            // При возвращении он сам в нее обратно добавится. 
            
            IPoolable takingElement;

            if (_poolDictionary.TryGetValue(typeof(T), out List<IPoolable> elementsPool))
            {
                takingElement = elementsPool.Find(x => x.GetType() == typeof(T));
                if (takingElement == null)
                    takingElement = Create<T>();
            }
            else
            {
                takingElement = Create<T>();    
            }
            
            takingElement.parent = this;
            takingElement.gameObject.SetActive(true);
            

            if (!_poolDictionary.TryGetValue(typeof(T), out elementsPool))
            {
                Debug.LogError("Pool must have type list at this point");
                return null;
            }

            elementsPool.Remove(takingElement);

            takingElement.isPooled = false;

            return (T) takingElement;
        }
        
        public void Restore(IPoolable element)
        {
            if(element.isPooled) return;

            element.isPooled = true;
            element.gameObject.SetActive(false);
            element.transform.SetParent(pooledObjectsFolder);
            _poolDictionary[element.GetType()].Add(element);
        }
    }
}
