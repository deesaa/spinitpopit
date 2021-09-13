using System;
using UnityEditor;
using UnityEngine;

namespace JDS
{
    [CustomEditor((typeof(UnityReactiveCoreObserver)))]
    public class UnityReactiveCoreObserverInspector : Editor
    {
        private UnityReactiveCoreObserver _observer;
        
        private void OnEnable()
        {
            _observer = target as UnityReactiveCoreObserver;
        }

        private void OnDisable()
        {
            _observer = null;
        }

        public override void OnInspectorGUI()
        {
            EditorUtility.SetDirty(target);
            Draw();
        }

        private void Draw()
        {
            foreach (var valuePair in _observer._valuePairs)
            {
                EditorGUILayout.SelectableLabel($"{valuePair.Value.Key}");

                for (int i = valuePair.Value.ValueHistory.Count; i > 0; i--)
                {
                    var value = valuePair.Value.ValueHistory[i-1];
                    
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space(10);
                    EditorGUILayout.LabelField(value.Value);
                    EditorGUILayout.LabelField(value.DateTime.ToString("mm:ss"));
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.Space(10);
            }
        }
    }
}