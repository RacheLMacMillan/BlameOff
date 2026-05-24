using System.Collections;
using Core.Scripts.Player.Checkers;
using Core.Scripts.Player.Gravitation;
using Core.Scripts.Player.Input;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
	public class PlayerVelocityController : MonoBehaviour
	{
		[Header("Player velocity")]
		[SerializeField] private float _playerVelocity;

		[Header("Gravity values")]
		[SerializeField] private float _inspectGravityValue;
		[SerializeField] private float _additionalInspectGravityValue;
		[SerializeField] private float _passiveStress;
		[SerializeField] private float _afterUpwardHitVelocity;
	
		[Header("Other values")]
		[SerializeField, Range(0,1)] public float _waitForSecondBeforeGravitate;
		
		[Header("Debugging")]
		[SerializeField] private bool _isDebugging;

		private PlayerInput _playerInput;
		private PlayerJumper _playerJumper;
		private PlayerGravitation _playerGravitation;
		private GroundedChecker _groundedChecker;
		private UpwardCollisionChecker _upwardCollisionChecker;
		private CharacterController _characterController;
	
		private bool _isPlayerJumping;
		private bool _isPlayerHittingFromAbove;

		private void OnEnable()
		{
			_playerInput.OnJumpInputted += StartGravitationCoroutine;
			_playerJumper.OnPlayerJumped += context => _playerVelocity = context;
			_upwardCollisionChecker.OnHitFromAbove += context => _isPlayerHittingFromAbove = context;
		}

		private void OnDisable()
		{
			_playerInput.OnJumpInputted -= StartGravitationCoroutine;
			_playerJumper.OnPlayerJumped -= context => _playerVelocity = context;
			_upwardCollisionChecker.OnHitFromAbove -= context => _isPlayerHittingFromAbove = context;
		}

		private void Awake()
		{
			_playerInput = GetComponent<PlayerInput>();
			_playerJumper = GetComponent<PlayerJumper>();
			_groundedChecker = GetComponent<GroundedChecker>();
			_upwardCollisionChecker = GetComponent<UpwardCollisionChecker>();
			_characterController = GetComponent<CharacterController>();
        
			_playerGravitation = new PlayerGravitation(_inspectGravityValue, _additionalInspectGravityValue, _passiveStress);
		}
    
		private void Update()
		{
			if (_isDebugging)
				Debug.Log(_groundedChecker.IsGrounded); 
    
			if (!_isPlayerJumping)
				_playerVelocity = _playerGravitation.BaseGravitate(_playerVelocity, _groundedChecker.IsGrounded);

			if (_isPlayerHittingFromAbove)
				_playerVelocity = _afterUpwardHitVelocity;
			
			_characterController.Move(new Vector3(0, _playerVelocity, 0) * Time.deltaTime);
		}
    
		private void StartGravitationCoroutine()
		{
			StartCoroutine(StopGravitationPlayerCoroutine());
		}

		private IEnumerator StopGravitationPlayerCoroutine()
		{
			_isPlayerJumping = true;
        
			if (_isDebugging)
				Debug.Log("Jumping true");
        
        
			yield return new WaitForSeconds(_waitForSecondBeforeGravitate);
        
			_isPlayerJumping = false;

			if (_isDebugging)
				Debug.Log("Jumping false");
		}
	}
}