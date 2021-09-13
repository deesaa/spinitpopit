using System;
using Client.UnityComponents;
using UnityEngine.UI;

namespace UnityComponents
{
    public class TextView : EntityBehaviour<TextRef>
    {
        public Text text;
        public TextType textType;

        private void Awake()
        {
            ref var baseComponent = ref GetBaseComponent();
            baseComponent.textType = textType;
            baseComponent.textView = this;
        }
        
        public void SetText(string text)
        {
            this.text.text = text;
        }
    }

    public struct TextRef
    {
        public TextType textType;
        public TextView textView;
    }

    public enum TextType
    {
        SpinsLeft,
        PopitTaken
    }
}