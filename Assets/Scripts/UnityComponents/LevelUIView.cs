using System;
using System.Collections;
using System.Collections.Generic;
using Client.ReactiveValues;
using Client.UnityComponents;
using Components;
using JDS;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIView : BindBehaviour
{
    public Text popitCounter;
    public Text spinsLeftCounter;

    private void Awake()
    {
        GRC<ValueTypes>.Bind(ValueTypes.PopitLevelStats, OnLevelStatsChange);
        
    }

    private void OnLevelStatsChange()
    {
        PopitLevelStats popitLevelStats 
            = GRC<ValueTypes>.Get<PopitLevelStats>(ValueTypes.PopitLevelStats);
        
        popitCounter.text =
            $"Popits Taken: {popitLevelStats.taken} / {popitLevelStats.count}";
    }

    private void OnDestroy()
    {
        GRC<ValueTypes>.Unbind(ValueTypes.PopitLevelStats, OnLevelStatsChange);
    }
}
