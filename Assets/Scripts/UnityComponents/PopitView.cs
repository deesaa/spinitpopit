using Leopotam.Ecs;
using UnityEngine;

namespace Client.UnityComponents
{
    public class PopitView : MonoBehaviour
    {
        public EcsEntity entity;
        public SpriteRenderer _spriteRenderer;

        public void OnTake()
        {
            _spriteRenderer.color = Color.red;;
        }
    }
}