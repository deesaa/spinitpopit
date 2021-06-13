using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JDS
{
    public static class DoTweenExt
    {
        public static Ease GetRandomEase()
        {
            return (Ease) Enum.GetValues(typeof(Ease)).GetValue(Random.Range(0, Enum.GetValues(typeof(Ease)).Length));
        }
        
        public static T SetRandomEase<T>(this T t, Ease[] filter, bool includeMode) where T : Tween
        {
            List<Ease> list = Ease.GetValues(typeof(Ease)).OfType<Ease>()
                .Where(x => filter.Contains(x) == includeMode && x < Ease.INTERNAL_Zero).ToList();
            return t.SetEase(list[Random.Range(0, list.Count)]);
        }
        
        public static T SetRandomEase<T>(this T t) where T : Tween
        {
            return t.SetEase((Ease) Random.Range(0, Ease.GetValues(typeof(Ease)).Length - 2));
        }
        
        public static Sequence DoRotateWithShacking(this Transform transform, Vector3 endValue, float duration,
            float strength = 3f, int vibrato = 10, float randomness = 90f,
            bool ignoreZAxis = true, bool fadeOut = true, RotateMode mode = RotateMode.FastBeyond360)
        {
            TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCoreRotator;
            TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> tweenerCoreShaker;

            Vector3 shackedVector = Vector3.zero;
            Quaternion rotation = transform.rotation;

            tweenerCoreRotator = DOTween.To(() => rotation, (r) => rotation = r, endValue, duration);
            tweenerCoreRotator.plugOptions.rotateMode = mode;

            tweenerCoreShaker = DOTween.Shake(() => shackedVector, (v) => shackedVector = v,
                duration, strength, vibrato, randomness, ignoreZAxis, fadeOut);

            return DOTween.Sequence()
                .Join(tweenerCoreShaker)
                .Join(tweenerCoreRotator).OnUpdate(() => transform.rotation = rotation * Quaternion.Euler(shackedVector));
        }
    }
}