namespace UnityComponents
{
    public interface IEvaluable<T, TV>
    { 
        T Evaluate(TV value);
    }
}