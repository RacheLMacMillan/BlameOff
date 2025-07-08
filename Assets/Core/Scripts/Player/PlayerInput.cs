using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private InputMap _inputMap;

	private PlayerMover _playerMover;
	private PlayerLooker _playerLooker;

	private void Awake()
	{
		_inputMap = new InputMap();

		_playerMover = GetComponent<PlayerMover>();
		_playerLooker = GetComponent<PlayerLooker>();
	}

	private void OnEnable() => _inputMap.Enable();

	private void OnDisable() => _inputMap.Disable();

	private void Update()
	{
		Vector2 moveDirection = _inputMap.PlayScene.Move.ReadValue<Vector2>();
		Vector3 correctedMoveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
		
		Vector2 lookDirection = _inputMap.PlayScene.Look.ReadValue<Vector2>();
		
		if (correctedMoveDirection != Vector3.zero)
		{
			_playerMover.MoveByTransformDirection(correctedMoveDirection);
		}
		
		if (lookDirection != Vector2.zero)
		{
			_playerLooker.Look(lookDirection);
		}
	}
}