using UnityEngine;

public class PlayerMover : MonoBehaviour, IInitializable, IMovable
{
	[field: SerializeField] public float MoveSpeed { get; private set; }
	
	private CharacterController _characterController;

	private void Awake()
	{
		Initialize();
	}
	
	public void Initialize()
	{
		_characterController = GetComponent<CharacterController>();
	}

	public void MoveByDirection(Vector2 direction)
	{
		// _characterController.Move(transform.TransformDirection(direction * MoveSpeed * Time.deltaTime));
	}
	
	public void MoveByTransformDirection(Vector3 direction)
	{		
		Vector3 scaledMoveDirection = direction * MoveSpeed * Time.deltaTime;
		
		_characterController.Move(transform.TransformDirection(scaledMoveDirection));
	}
	
	public void MoveToTheTarget(GameObject target)
	{
		
	}
}