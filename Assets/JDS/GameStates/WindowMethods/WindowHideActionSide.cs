using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEditor;
using UnityEngine;

namespace JDS
{
    [CreateAssetMenu(menuName = "Window Show-Hide/WindowHideSide", fileName = "HideSide", order = 0)]
    public class WindowHideActionSide : IWindowHideAction
    {
        public float showSpeed = 1f;
        public float biasMultiplier = 1f;
        public Ease easeType = Ease.InQuad;
        public Side showType = Side.Right;
        
        private TweenerCore<Vector3, Vector3, VectorOptions> _currentTween;
        
        public override void Apply(Transform windowContainer)
        {
            _currentTween = windowContainer
                .DOMove(WindowUtil.GetHiddenPositionOnSide(showType) * biasMultiplier, showSpeed)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    windowContainer.gameObject.SetActive(false);
                });
        }

        public override void Break()
        {
            _currentTween?.Kill();
        }
    }
}