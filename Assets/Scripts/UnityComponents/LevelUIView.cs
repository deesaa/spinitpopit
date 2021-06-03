using System;
using System.Collections;
using System.Collections.Generic;
using Client.UnityComponents;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIView : MonoBehaviour
{
    public Text popitCounter;
    public Text spinsLeftCounter;

    private void Awake()
    {
        RuntimeData.OnPopitCountChanged += OnPopitCountChange;
        RuntimeData.OnPopitTakenChanged += OnPopitTakenChange;
    }

    private void OnPopitCountChange(PopitLevelStats stats)
    {
        popitCounter.text = $"Popits Taken: {stats.taken} / {stats.count}";
    }
    
    private void OnPopitTakenChange(PopitLevelStats stats)
    {
        popitCounter.text = $"Popits Taken: {stats.taken} / {stats.count}";

    }

    private void OnDestroy()
    {
        RuntimeData.OnPopitCountChanged -= OnPopitCountChange;
        RuntimeData.OnPopitTakenChanged -= OnPopitTakenChange;
    }
}
