using System;
using Components;
using UnityEngine;

namespace Client.UnityComponents
{
    public class PopitView : EntityBehaviour<PopitRef>
    {
        public SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            GetBaseComponent().popitView = this;
        }

        public void OnTake()
        {
            _spriteRenderer.color = Color.red;;
        }

        public void Reset()
        {
            _spriteRenderer.color = Color.white;;
        }
    }
}