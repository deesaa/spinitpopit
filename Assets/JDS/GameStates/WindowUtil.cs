using System;
using UnityEngine;

namespace JDS
{
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
        public static Vector3 GetHiddenPositionOnSide(Side side)
        {
            switch (side)
            {
                case Side.Right:
                    return Vector3.right * WorldCameraWidth;
                case Side.Left:
                    return Vector3.left * WorldCameraWidth;
                case Side.Top:
                    return Vector3.up * WorldCameraHeight;
                case Side.Bottom:
                    return Vector3.down * WorldCameraHeight;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public enum Side
    {
        Right, Left, Top, Bottom
    }
}