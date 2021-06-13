using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class DeleteSystem : IEcsRunSystem
    {
        private EcsFilter<Delete> _filter;  
        
        public void Run()
        {
            foreach (int index in _filter)
            {
                ref Delete delete = ref _filter.Get1(index);
                delete.timePassed += Time.deltaTime;
                if (delete.timePassed >= delete.deleteDelay)
                {
                    if(delete.gameObject != null)
                        Object.Destroy(delete.gameObject);
                    _filter.GetEntity(index).Destroy();
                }
            }
        }
    }
}