namespace SR
{
    public abstract class FuncSO<T1> : SploveScriptableObject
    where T1 : class
    {
        public abstract void Initialize(T1 owner);
        public abstract void Execute(T1 owner);
    }

    public abstract class FuncSO<T1, T2> : SploveScriptableObject
    where T1 : class
    where T2 : struct
    {
        public abstract void Initialize(T1 owner);
        public abstract void Execute(T1 owner, T2 option);
    }
}
