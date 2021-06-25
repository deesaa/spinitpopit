using UnityEngine;

namespace Client.Components
{
    public struct InputEvent
    {
        public InputType InputType;
        public Vector2 touchPoint;
    }

    public enum InputType
    {
        Space,
        Touch
    }
}