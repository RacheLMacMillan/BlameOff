public interface IMoveable
{
    public void Move();
}

public interface IMoveable<T>
{
    public void Move(T t);
}