using JDS;
using Leopotam.Ecs;
using UnityComponents;

namespace Client.Systems.Observe
{
    public class LevelStatsCounterObserveSystem : EcsObserveSystem<string>
    {
        private EcsFilter<TextRef> _filter;
        
        public override void Init()
        {
            Bind("SpinsLeft", OnSpinsLeftChanged, Model.Get);
            Bind("PopitTaken", OnPopitTakenChanged, Model.Get);
        }

        private void OnSpinsLeftChanged(object value)
        {
            foreach (int index in _filter)
            {
                if (_filter.Get1(index).textType == TextType.SpinsLeft)
                {
                    _filter.Get1(index).textView.SetText($"Spins Left: {(string) value}");
                }
            }
        }
        
        private void OnPopitTakenChanged(object value)
        {
            foreach (int index in _filter)
            {
                if (_filter.Get1(index).textType == TextType.PopitTaken)
                {
                    _filter.Get1(index).textView.SetText($"Popit Taken: {(string) value}");
                }
            }
        }
    }
}