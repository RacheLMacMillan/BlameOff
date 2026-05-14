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

    void OnEnable() => _playerInput.OnJumpInputted += Jump;
    void OnDisable() => _playerInput.OnJumpInputted -= Jump;

    public void Jump()
	{
		if (_groundedChecker.IsGrounded == false)
			throw new ArgumentException("Player must be on the ground before jumping.");
				
		transform.position += new Vector3(0, 0.1f, 0);
		
		playerLocalVelocity = Mathf.Sqrt(-_jumpForce * -9.8f);
		
		OnPlayerJumped?.Invoke(playerLocalVelocity);
		
		Debug.Log("Player was jumped");
	}
}