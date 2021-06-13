using Client.ReactiveValues;
using DG.Tweening;
using JDS;

namespace Client.States
{
    public class TransitionState : IGameState
    {
        public void OnEnter()
        {
            StateType stateType = GRC<RValueType>.Get<StateType>(RValueType.NextState);
            
            /*DOVirtual.DelayedCall(0.15f, () =>
            {
                GSM<StateType>.Get.ChangeOn(stateType);
            });*/
            NGSM<StateType>.Get.ChangeOn(stateType);
        }

        public void OnExit()
        {
            
        }

        public void StateMessage(string name)
        {
            
        }
    }
}