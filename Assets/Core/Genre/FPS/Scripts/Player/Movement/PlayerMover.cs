using UnityEngine;

namespace FPS
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        [SerializeField] private bool _isDebugging;

        private PlayerInput _playerInput;
    
        private CharacterController _characterController;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable() => _playerInput.OnMoveInputted += Move;
        private void OnDisable() => _playerInput.OnMoveInputted -= Move;

        private void Move(Vector3 direction)
        {
            Vector3 scaledMoveDirection = direction * _speed * Time.deltaTime;
		
            _characterController.Move(transform.TransformDirection(scaledMoveDirection));

            if (_isDebugging)
                Debug.Log("Moved");
        }
    }
}