using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private InputMap _inputMap;

	private PlayerMover PlayerMover;

	private void Awake()
	{
		_inputMap = new InputMap();

		PlayerMover = GetComponent<PlayerMover>();
	}

	private void OnEnable() => _inputMap.Enable();

	private void OnDisable() => _inputMap.Disable();

	private void Update()
	{
		Vector2 moveDirection = _inputMap.OnFoot.Move.ReadValue<Vector2>();
		Vector3 correctedMoveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);

		PlayerMover.MoveByTransformDirection(correctedMoveDirection);
	}
}