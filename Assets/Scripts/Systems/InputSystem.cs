using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        public void Run()
        {
            if (Input.GetKey(KeyCode.E) || Input.touchCount > 0)
            {
                _world.NewEntity().Get<InputEvent>().InputType = InputType.Space;
            }
        }
    }
}