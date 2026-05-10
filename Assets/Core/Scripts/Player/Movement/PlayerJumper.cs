using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IJumpable
{
	[SerializeField] private float _jumpForce;
	
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

    private void Update()
    {
		playerVelocity = _gravitation.GravitatePlayer(playerVelocity, _groundedChecker.IsGrounded);
    
		_characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
	{
		if (_groundedChecker.IsGrounded == false)
			throw new ArgumentException("Player must be on the ground before jumping.");
				
		playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f);
	}
}