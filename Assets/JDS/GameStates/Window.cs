using System;
using Client.States;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Leopotam.Ecs;
using UnityEngine;

namespace JDS
{
    public abstract class Window<T, TV> : BindBehaviour<TV>, IWindow
    {
        public float showSpeed = 1f;
        public Ease easeType = Ease.InQuad;
        public WindowShowType showType = WindowShowType.Right;
        public T windowType;
        public Transform container;
        private TweenerCore<Vector3, Vector3, VectorOptions> _currentTween;

        private void Awake()
        {
            container.gameObject.SetActive(false);
            container.position = GetHiddenPosition();
            WM<T>.RegisterWindow(windowType, this);
            OnAwake();
        }
        
        protected virtual void OnAwake() {}

        public void Show()
        {
            OnShow();

            if (showType == WindowShowType.Center)
            {
                _currentTween?.Kill();
                container.position = GetHiddenPosition();
                container.gameObject.SetActive(true);
                return;
            }

            container.gameObject.SetActive(true);
            _currentTween?.Kill();
            _currentTween = container.DOMove(Vector3.zero, showSpeed).SetEase(easeType);
            
        }

        protected virtual void OnShow() {}

        public void Hide()
        {
            OnHide();

            if (showType == WindowShowType.Center)
            {
                _currentTween?.Kill();
                container.gameObject.SetActive(false);
                container.position = GetHiddenPosition();
                return;
            }
            
            _currentTween?.Kill();
            _currentTween = container.DOMove(GetHiddenPosition(), showSpeed).SetEase(easeType).OnComplete(() =>
            {
                container.gameObject.SetActive(false);
            });
        }

        protected virtual void OnHide() { }

        private Vector3 GetHiddenPosition()
        {
            switch (showType)
            {
                case WindowShowType.Center:
                    return Vector3.zero;
                case WindowShowType.Right:
                    return Vector3.right * WindowUtil.WorldCameraWidth;
                case WindowShowType.Left:
                    return Vector3.left * WindowUtil.WorldCameraWidth;
                case WindowShowType.Top:
                    return Vector3.up * WindowUtil.WorldCameraHeight;
                case WindowShowType.Bottom:
                    return Vector3.down * WindowUtil.WorldCameraHeight;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum WindowShowType
    {
        Center, Right, Left, Top, Bottom
    }

    public static class WindowUtil
    {
        private static Camera _mainCamera;
        public static Camera MainCamera
        {
            get
            {  
                if(_mainCamera == null)
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
        }
        public static float WorldCameraHeight => MainCamera.orthographicSize * 2f;
        public static float WorldCameraWidth => WorldCameraHeight * MainCamera.aspect;
    }
}