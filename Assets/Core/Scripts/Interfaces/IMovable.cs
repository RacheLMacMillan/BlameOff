using UnityEngine;

public interface IMovable
{
	public void MoveByDirection(Vector2 direction);
	public void MoveToTheTarget(GameObject target);
}