namespace Core.Genre.FPS.Scripts.Interfaces
{
    public interface IMoveable
    {
        public void Move();
    }

    public interface IMoveable<Vector3>
    {
        public void Move(Vector3 direction);
    }
}