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
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("SPACE DOWN");
                _world.NewEntity().Get<InputEvent>().InputType = InputType.Space;
            }
        }
    }
}