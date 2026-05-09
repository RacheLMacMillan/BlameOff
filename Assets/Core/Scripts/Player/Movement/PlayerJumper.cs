using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IJumpable
{
	[SerializeField] private float _jumpForce;
	
	[SerializeField] private Vector3 _jumpingStartUp;
	[SerializeField] private Vector3 playerVelocity;
	
    private PlayerInput _playerInput;
    private GroundedChecker _groundedChecker;
	private CharacterController _characterController;
	
	private Gravitation _gravitation;

	public void Awake()
	{
        _playerInput = GetComponent<PlayerInput>();
        _groundedChecker = GetComponent<GroundedChecker>();
		_characterController = GetComponent<CharacterController>();
		
		_gravitation = new Gravitation();
	}

    void OnEnable() => _playerInput.OnJumpInputted += Jump;
    void OnDisable() => _playerInput.OnJumpInputted -= Jump;

    public void Jump()
	{
		if (_groundedChecker.IsGrounded == false)
			throw new ArgumentException("Player must be on the ground before jumping.");
		
		transform.position += _jumpingStartUp;
		
		playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f);
		
		// _player.PlayerVelocityViewModel.Value = playerVelocity;
		
		// _characterController.Move(playerVelocity * Time.deltaTime);
	}

    private void Update()
    {
		_characterController.Move(_gravitation.GravitatePlayer(playerVelocity, _groundedChecker.IsGrounded) * Time.deltaTime);
    }
    
    
}