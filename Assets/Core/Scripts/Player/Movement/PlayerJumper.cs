using System;
using Core.Scripts.Player.Checkers;
using Core.Scripts.Player.Input;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
	public class PlayerJumper : MonoBehaviour
	{
		[SerializeField] private float _jumpForce;
	
		private float _playerLocalVelocity;
	
		private PlayerInput _playerInput;
		private GroundedChecker _groundedChecker;
		private PlayerCharacterHeightController _playerCharacterHeightController;
	
		public Action<float> OnPlayerJumped;
		public Action OnPlayerNeedToStandUp;
		
		[SerializeField] private bool _isDebugging;

		public void Awake()
		{
			_playerInput = GetComponent<PlayerInput>();
			_groundedChecker = GetComponent<GroundedChecker>();
			_playerCharacterHeightController = GetComponent<PlayerCharacterHeightController>();
		}

		void OnEnable() => _playerInput.OnJumpInputted += Jump;
		void OnDisable() => _playerInput.OnJumpInputted -= Jump;

		private void Jump()
		{
			if (_groundedChecker.IsGrounded == false)
				throw new ArgumentException("Player must be on the ground before jumping.");

			if (_playerCharacterHeightController.IsStanding)
			{
				_playerLocalVelocity = Mathf.Sqrt(-_jumpForce * -9.8f);
				
				OnPlayerJumped?.Invoke(_playerLocalVelocity);
				
				if (_isDebugging)
					Debug.Log("Player was jumped " + _playerLocalVelocity);
			}
			else
			{
				OnPlayerNeedToStandUp?.Invoke();
				
				if (_isDebugging)
					Debug.Log("Player didn't jumped, He needs to stand up");
			}
			
		}
	}
}