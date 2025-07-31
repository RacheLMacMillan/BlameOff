public interface IInitializable
{
    public void Initialize();
}

public interface IInitializable<T>
{
    public void Initialize(T t);
}

public interface IInitializable<T0, T1>
{
    public void Initialize(T0 T0, T1 T1);
}