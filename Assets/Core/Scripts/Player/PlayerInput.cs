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
		Vector3 scaledDirection = new Vector3(_inputMap.OnFoot.Move.ReadValue<Vector2>().x, 0, _inputMap.OnFoot.Move.ReadValue<Vector2>().y);

		PlayerMover.MoveByTransformDirection(scaledDirection);
	}
}