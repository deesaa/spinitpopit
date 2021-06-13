using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JDS
{
    public static class Extensions
    {
        public static string GetFullName(this Transform transform)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(transform.name);
            
            while (transform.parent != null)
            {
                transform = transform.parent;
                sb.Append(".").Append(transform.name);
            }
            
            return sb.ToString();
        }

        public static void CopyTo(this Transform transformIn, Transform transformOut)
        {
            transformOut.position = transformIn.position;
            transformOut.rotation = transformIn.rotation;
            transformOut.localScale = transformIn.localScale;
        }

        public static Vector2 TakeRandomPos(this List<Transform> list)
        {
            return list[Random.Range(0, list.Count)].position;
        }
        
        public static T GetRandom<T>(this List<T> list) 
        {
            return list[Random.Range(0, list.Count)];
        }
    }

    public class ViewRect
    {
        private Rect _rect;

        public Vector2 TopLeft => new Vector3(_rect.xMax, _rect.yMin);
        public Vector2 BottomLeft => new Vector3(_rect.yMin, _rect.yMin);
        public Vector2 TopRight => new Vector3(_rect.xMax, _rect.yMax);
        public Vector2 BottomRight => new Vector3(_rect.xMin, _rect.yMax);

        public Vector2 RightSide => BottomRight + TopRight;
        public Vector2 LeftSide => BottomRight + TopRight;
        public Vector2 BottomSide => BottomRight + TopRight;
        public Vector2 TopSide => BottomRight + TopRight;
    
        public float RightSize => RightSide.x;
        public float LeftSize => LeftSide.x;
        public float BottomSize => BottomSide.y;
        public float TopSize => TopSide.y;

        public ViewRect(Rect rect) => _rect = rect;
    }
    
    
    
}