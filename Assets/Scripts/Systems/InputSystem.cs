using Client.Components;
using Client.States;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        public void Run()
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(GSM<StateType>.Get.CurrentStateType == StateType.Level)
                    _world.NewEntity().Get<InputEvent>().InputType = InputType.Space;
            }
        }
    }
}