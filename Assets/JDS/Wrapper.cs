namespace JDS
{
    public class Wrapper<T> where T : struct
    {
        public T Value;

        public Wrapper(T value)
        {
            Value = value;
        }
    }
}