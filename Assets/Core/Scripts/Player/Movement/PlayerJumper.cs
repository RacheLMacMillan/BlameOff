using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IJumpable
{
	[SerializeField] private float _jumpForce;
	
	private float playerLocalVelocity;
	
    private PlayerInput _playerInput;
    private GroundedChecker _groundedChecker;
	
	public Action<float> OnPlayerJumped;

	public void Awake()
	{
        _playerInput = GetComponent<PlayerInput>();
        _groundedChecker = GetComponent<GroundedChecker>();
	}

    // void OnEnable() => _playerInput.OnJumpInputted += Jump;
    // void OnDisable() => _playerInput.OnJumpInputted -= Jump;

    public float Jump()
	{
		if (_groundedChecker.IsGrounded == false)
			throw new ArgumentException("Player must be on the ground before jumping.");
		
		playerLocalVelocity = Mathf.Sqrt(-_jumpForce * -9.8f);
		
		Debug.Log("Player was jumped " + playerLocalVelocity);
		return playerLocalVelocity;
		// OnPlayerJumped?.Invoke(playerLocalVelocity);
		
	}
}