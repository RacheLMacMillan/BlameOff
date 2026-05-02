using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private float _speed;
    
    private CharacterController _characterController;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable() => _playerInput.OnPlayerMoved += Move;
    private void OnDisable() => _playerInput.OnPlayerMoved -= Move;

    private void Move(Vector3 direction)
    {
        Vector3 scaledMoveDirection = direction * _speed * Time.deltaTime;
		
		_characterController.Move(transform.TransformDirection(scaledMoveDirection));
		
		Debug.Log("Moved");
    }
}