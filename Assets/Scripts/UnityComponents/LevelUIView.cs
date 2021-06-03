using System;
using System.Collections;
using System.Collections.Generic;
using Client.ReactiveValues;
using Client.UnityComponents;
using Components;
using JDS;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIView : MonoBehaviour
{
    public Text popitCounter;
    public Text spinsLeftCounter;

    private void Awake()
    {
        ReactiveCoreG<ValueTypes>.Bind(ValueTypes.PopitLevelStats, OnLevelStatsChange);
    }

    private void OnLevelStatsChange()
    {
        PopitLevelStats popitLevelStats 
            = ReactiveCoreG<ValueTypes>.Get<PopitLevelStats>(ValueTypes.PopitLevelStats);
        
        popitCounter.text =
            $"Popits Taken: {popitLevelStats.taken} / {popitLevelStats.count}";
    }

    private void OnDestroy()
    {
        ReactiveCoreG<ValueTypes>.Unbind(ValueTypes.PopitLevelStats, OnLevelStatsChange);
    }
}
