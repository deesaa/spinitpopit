using System;
using Components;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;

namespace Client.UnityComponents
{
    public class SpinnerView : MonoBehaviour
    {
        public EcsEntity entity;
        public Transform body;
        public AimView aimView;
        public Rigidbody2D rigidbody2D;

        public Collider2D collider2D;

        public AimMethodType aimMethodType;

        public AnimationCurve spinTimeToRotateSpeed;
        public float rotateMultiplier;
        public AnimationCurve spinTimeToSpeed;
        public float speedMultiplier;
        public AnimationCurve spinTimeAfterReleaseToSpinTime01;

        public float maxSpinTime;
        public float minSpinTimeForRelease;

        public IScriptFloatCurve spinTimeToRotateSpeed_;

        private void OnCollisionEnter2D(Collision2D other)
        {
            entity.Get<SpinnerRef>().currentDirection = 
                Vector3.Reflect(entity.Get<SpinnerRef>().currentDirection, other.contacts[0].normal);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PopitView popitView = other.GetComponent<PopitView>();
            if (popitView != null)
            {
                popitView.Entity.Get<TriggerEvent>().triggerType = TriggerType.SpinnerEnter;
            }
        }

        public void DisableInteraction()
        {
            collider2D.enabled = false;
        }
    }
}