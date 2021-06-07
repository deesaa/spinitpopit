namespace Components
{
    public struct GameEvent
    {
        public GameEventType gameEventType;
    }

    public enum GameEventType
    {
        LoadLevel,
        LevelRestart
    }
}