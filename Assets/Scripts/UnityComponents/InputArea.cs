using System;
using Client.Components;
using Client.States;
using JDS;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using InputEvent = Client.Components.InputEvent;

namespace Client.UnityComponents
{
    public class InputArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private EcsWorld _ecsWorld;
        private bool _isPointerDown = false;
        public GameData gameData;

        public void SetEcsWorld(EcsWorld world)
        {
            _ecsWorld = world;
        }

        private void Update()
        {
            if(!_isPointerDown || GSM<StateType>.Get.CurrentStateType != StateType.Level)
                return;
            
            _ecsWorld.NewEntity().Get<InputEvent>().InputType = InputType.Space;
            
            Vector3 touchPosition = gameData.mainCamera.ScreenToWorldPoint(Input.mousePosition);

            InputEvent inputEvent = new InputEvent();
            inputEvent.InputType = InputType.Touch;
            inputEvent.touchPoint = touchPosition;

            _ecsWorld.NewEntity().Replace(inputEvent);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
        }
    }
}