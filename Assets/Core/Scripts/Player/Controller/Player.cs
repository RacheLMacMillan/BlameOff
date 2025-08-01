using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerLooker))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(IsGroundedChecker))]
[RequireComponent(typeof(IsObstacleAboveChecker))]
public class Player : MonoBehaviour
{
    [Header("Core settings")]
    [field: SerializeField] public float Health { get; private set; }
    
    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Min(1), Range(1, 10)] public float SpringSpeed { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float CrouchSpeed { get; private set; }
    
    [field: SerializeField, Min(0)] public float JumpForce { get; private set; }
    
    [field: SerializeField] public bool IsGrounded { get; private set; }
    [field: SerializeField] public bool IsObstacleAbove { get; private set; }
    
    [Header("User settings")]
    [field: SerializeField, Min(0), Range(0, 1)] public float XSensitivity { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float YSensitivity { get; private set; }

    [Header("Do not touch!")]
    [field: SerializeField] public Vector3 PlayerVelocity { get; set; }

    [Header("Gravitation")]
    [SerializeField] private float _inspectGravityValue;
    [SerializeField] private float _passiveStress;
    
    [Header("Debug settings")]
    [SerializeField] private bool _isDebuggingOn;
    
    [Header("Components")]
    [field: SerializeField] public Camera Camera { get; private set; }
    
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    
    [field: SerializeField] public IsGroundedChecker IsGroundedChecker { get; private set; }
    [field: SerializeField] public IsObstacleAboveChecker IsObstacleAboveChecker { get; private set; }
    [field: SerializeField] public JumpCollisionDetector JumpCollisionDetector { get; private set; }

    [field: SerializeField] public PlayerGravitation PlayerGravitation { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerLooker PlayerLooker { get; private set; }
    [field: SerializeField] public PlayerMover PlayerMover { get; private set; }
    [field: SerializeField] public PlayerJumper PlayerJumper { get; private set; }
    
    public event Action<float> OnMoveSpeedChanged;
    public event Action<float, float> OnCameraSettingsChanged;
    public event Action<float> OnJumpingSettingsChanged;
    
    private void Awake()
    {    
        GetPlayersComponents();
        InitializePlayer();
    }

    private void OnEnable()
    {
        PlayerInput.OnPlayerLooking += OnLook;
        PlayerInput.OnPlayerMoving += OnMove;
        JumpCollisionDetector.PlayerCollideWithSomethingFromAbove += ResetVelocityByY;
    }

    private void OnDisable()
    {
        PlayerInput.OnPlayerLooking -= OnLook;
        PlayerInput.OnPlayerMoving -= OnMove;
        JumpCollisionDetector.PlayerCollideWithSomethingFromAbove -= ResetVelocityByY;
    }

    private void Update()
    {
        IsGrounded = IsGroundedChecker.IsGrounded();
        IsObstacleAbove = IsObstacleAboveChecker.IsObstaclesAbove();
        
        JumpCollisionDetector.DetectCollisionFromAbove(IsGrounded, IsObstacleAbove);
    
        PlayerInput.UpdateInput();
        UpdateVelocity(PlayerGravitation.Gravitate(PlayerVelocity, IsGrounded, _inspectGravityValue, _passiveStress));
    }

    public void OnLook(Vector2 delta) 
    {
        PlayerLooker.Look(delta);
    }
    
    public void OnMove(Vector3 direction)
    {
        PlayerMover.Move(direction);
    }

    public void OnSprint() {}
    public void OnCrouch() {}
    
    public void OnJump()
    {
        UpdateVelocity(PlayerJumper.Jump(PlayerVelocity, IsGrounded, IsObstacleAbove).y);
    }

    public void SetSettings()
    {
        ChangeMoveSpeed(MoveSpeed);
        ChangeCameraSettings(XSensitivity, YSensitivity);
        ChangeJumpingSettings(JumpForce);
        
        if (_isDebuggingOn)
            Debug.Log("Core Setting were set.");
    }
    
    public void ChangeCameraSettings(float xSensitivity, float ySensitivity)
    {
        XSensitivity = xSensitivity;
        YSensitivity = ySensitivity;

        OnCameraSettingsChanged?.Invoke(XSensitivity, YSensitivity);
        
        if (_isDebuggingOn)
            Debug.Log($"Camera's settings were changed. Horizontal sensitivity equals {XSensitivity} and Vertical  sensitivity equals {YSensitivity}.");
    }
    
    private void ChangeMoveSpeed(float value)
    {
        MoveSpeed = value;

        OnMoveSpeedChanged?.Invoke(MoveSpeed);

        if (_isDebuggingOn)
            Debug.Log($"Move speed was changed. It equals {MoveSpeed}.");
    }
    
    private void ChangeJumpingSettings(float jumpForce)
    {
        JumpForce = jumpForce;

        OnJumpingSettingsChanged?.Invoke(JumpForce);
        
        if (_isDebuggingOn)
            Debug.Log($"Jumping setting were changed. Jump force equals {JumpForce}");
    }
    
    private void UpdateVelocity(float velocityByY)
    {
        PlayerVelocity = new Vector3(PlayerVelocity.x, velocityByY, PlayerVelocity.z);
    }
    
    private void ResetVelocityByY()
    {
        PlayerVelocity = new Vector3(PlayerVelocity.x, -1, PlayerVelocity.z);
    }
    
    private void GetPlayersComponents()
    {
        Camera = GetComponentInChildren<Camera>();
        
        CharacterController = GetComponent<CharacterController>();
        
        IsGroundedChecker = GetComponent<IsGroundedChecker>();
        IsObstacleAboveChecker = GetComponent<IsObstacleAboveChecker>();
        
        JumpCollisionDetector = new JumpCollisionDetector();

        PlayerGravitation = new PlayerGravitation(CharacterController);
    
        PlayerInput = GetComponent<PlayerInput>();
        PlayerLooker = GetComponent<PlayerLooker>();
        PlayerMover = GetComponent<PlayerMover>();
        PlayerJumper = GetComponent<PlayerJumper>();
        
        if (_isDebuggingOn)
            Debug.Log("The components are gotten.");
    }
    
    private void InitializePlayer()
    {
        PlayerInput.Initialize(this);
        PlayerLooker.Initialize(this);
        PlayerMover.Initialize(this);
        PlayerJumper.Initialize(this);
        
        if (_isDebuggingOn)
            Debug.Log("The components are initialized.");
    }
}