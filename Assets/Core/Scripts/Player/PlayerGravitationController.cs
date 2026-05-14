using System.Collections;
using UnityEngine;

public class PlayerGravitationController : MonoBehaviour
{
    [Header("Player velocity")]
    [SerializeField] private float _playerVelocity;

    [Header("Gravity values")]
    [SerializeField] private float _inspectGravityValue;
	[SerializeField] private float _passiveStress;
	
	[Header("Other values")]
	[field: SerializeField, Range(0,1)] public float _waitForSecondBeforeGravitate;

    private PlayerInput _playerInput;
    private PlayerGravitation _playerGravitation;
    private GroundedChecker _groundedChecker;
	private CharacterController _characterController;
	
	private bool _isPlayerJumping;

    private void OnEnable() => _playerInput.OnJumpInputted += StartGravitationCoroutine;
    private void OnDisable() => _playerInput.OnJumpInputted -= StartGravitationCoroutine;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _groundedChecker = GetComponent<GroundedChecker>();
        _characterController = GetComponent<CharacterController>();
        
        _playerGravitation = new PlayerGravitation(_inspectGravityValue, _passiveStress);
    }
    
    private void Update()
    {
        if (_isPlayerJumping == false)
		    _playerVelocity = _playerGravitation.GravitatePlayer(_playerVelocity, _groundedChecker.IsGrounded);
    
		_characterController.Move(new Vector3(0, _playerVelocity, 0) * Time.deltaTime);
    }
    
    private void StartGravitationCoroutine()
    {
        StopGravitationPlayerCoroutine();
    }

    private IEnumerator StopGravitationPlayerCoroutine()
    {
        _isPlayerJumping = true;
        
        yield return new WaitForSeconds(_waitForSecondBeforeGravitate);
        
        _isPlayerJumping = false;
    }
}