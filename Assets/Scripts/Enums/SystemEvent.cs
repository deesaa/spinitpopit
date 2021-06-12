namespace Components
{
    public struct SystemEvent
    {
        public SystemEventType systemEventType;

        public override bool Equals(object obj)
        {
            return obj != null && ((SystemEvent) obj).systemEventType == systemEventType;
        }

        public override int GetHashCode()
        {
            return (int) systemEventType;
        }
    }

    public enum SystemEventType
    {
        LoadLevel
    }
    
    
    //TODO: ??? СДЕЛАТЬ В КАЖДОМ СТЭЙТЕ ГРУППУ ИНИТ СИСТЕМ, ЗАПУСКАЕМЫХ ПРИ ВХОДЕ В СТЭЙТ ????  
}