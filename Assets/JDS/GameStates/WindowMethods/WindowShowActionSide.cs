using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using UnityEngine;

namespace JDS
{
    
    [CreateAssetMenu(menuName = "Window Show-Hide/WindowShowSide", fileName = "ShowSide", order = 0)]
    public class WindowShowActionSide : IWindowShowAction
    {
        public float showSpeed = 1f;
        public float biasMultiplier = 1f;
        public Ease easeType = Ease.InQuad;
        public Side showType = Side.Right;
        
        private TweenerCore<Vector3, Vector3, VectorOptions> _currentTween;

        public override void SetStart(Transform windowContainer)
        {
            var position = WindowUtil.GetHiddenPositionOnSide(showType) * biasMultiplier;

            windowContainer.gameObject.SetActive(false);
            windowContainer.position = position;
        }
        
        public override void Apply(Transform windowContainer)
        {
            windowContainer.gameObject.SetActive(true);
            _currentTween = windowContainer.DOMove(Vector3.zero, showSpeed).SetEase(easeType);
        }

        public override void Break()
        {
            _currentTween?.Kill();
        }
    }
}