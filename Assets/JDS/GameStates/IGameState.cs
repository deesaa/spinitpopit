namespace JDS
{
    public interface IGameState
    { 
        void OnEnter();
        void OnExit();
        void OnEvent(string name);
    }
}