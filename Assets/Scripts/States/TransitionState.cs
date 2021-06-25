using Client.ReactiveValues;
using DG.Tweening;
using JDS;

namespace Client.States
{
    public class TransitionState : IGameState
    {
        public void OnEnter()
        {
            StateType stateType = RC<RValueType>.Get<StateType>(RValueType.NextState);
            DOVirtual.DelayedCall(0.35f, () =>
            { 
                GSM<StateType>.Get.ChangeOn(stateType);
            });
        }

        public void OnExit()
        {
           
        }

        public void MovedForward()
        {
            
        }

        public void MovedBack()
        {
          
        }
    }
}