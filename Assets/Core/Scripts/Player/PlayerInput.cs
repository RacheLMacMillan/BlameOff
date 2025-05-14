using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private InputMap _inputMap;

	private void Awake()
	{
		_inputMap = new InputMap();
	}

	private void OnEnable() => _inputMap.Enable();

	private void OnDisable() => _inputMap.Disable();

	private void Update()
	{
		Move(_inputMap.OnFoot.Move.ReadValue<Vector2>());
	}

    private void Move(Vector2 direction)
    {
        Debug.Log($"Player was moves by {direction}");
    }
}