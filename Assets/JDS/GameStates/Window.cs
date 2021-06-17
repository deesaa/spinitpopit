using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace JDS
{
    public abstract class Window<T, TV> : ObservedBehaviour<TV>, IWindow
    {
        public T windowType;
        public Transform container;

        public IWindowShowAction windowShowAction;
        public IWindowHideAction windowHideAction;

        protected sealed override void AfterAwake()
        {
            SetGroupName(windowType.ToString());
            windowShowAction.SetStart(container);
            WM<T>.RegisterWindow(windowType, this);
            OnAwake();
        }
        
        protected virtual void OnAwake() {}

        public void Show()
        {
            OnShow();
            windowHideAction.Break();
            windowShowAction.SetStart(container);
            windowShowAction.Apply(container);
        }

        protected virtual void OnShow() {}

        public void Hide()
        {
            OnHide();
            windowShowAction.Break();
            windowHideAction.Apply(container);
        }

        protected virtual void OnHide() { }
    }
}