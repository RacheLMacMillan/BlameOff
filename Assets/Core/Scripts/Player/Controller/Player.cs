using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerLooker))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [Header("Core settings")]
    [field: SerializeField] public float Health { get; private set; }
    
    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Min(1), Range(1, 10)] public float SpringSpeed { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float CrouchSpeed { get; private set; }
    
    [field: SerializeField, Min(0)] public float JumpForce { get; private set; }
    [field: SerializeField, Min(0)] public Vector3 JumpStartUp { get; private set; }
    
    [Header("User settings")]
    [field: SerializeField, Min(0), Range(0, 1)] public float XSensitivity { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float YSensitivity { get; private set; }

    [Header("Do not touch!")]
    [field: SerializeField] public Vector3 PlayerVelocity { get; set; }

    [Header("Gravitation")]
    [SerializeField] private float _inspectGravityValue;
    [SerializeField] private float _passiveStress;
    
    [field: SerializeField] public bool IsPlayerGrounded { get; private set; }
    
    [Header("Debug settings")]
    [SerializeField] private bool _isDebuggingOn;
    
    public event Action<float> OnMoveSpeedChanged;
    public event Action<float, float> OnCameraSettingsChanged;
    public event Action<Vector3, float> OnJumpingSettingsChanged;
    
    [Header("Components")]
    [field: SerializeField] public Camera Camera { get; private set; }
    
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public IsGroundedChecker IsGroundedChecker { get; private set; }

    [field: SerializeField] public PlayerGravitation PlayerGravitation { get; private set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerLooker PlayerLooker { get; private set; }
    [field: SerializeField] public PlayerMover PlayerMover { get; private set; }
    [field: SerializeField] public PlayerJumper PlayerJumper { get; private set; }
    
    private void Awake()
    {    
        GetPlayersComponents();
        InitializePlayer();
    }

    private void OnEnable()
    {
        PlayerInput.OnPlayerLooking += OnLook;
        PlayerInput.OnPlayerMoving += OnMove;
    }

    private void OnDisable()
    {
        PlayerInput.OnPlayerLooking -= OnLook;
        PlayerInput.OnPlayerMoving -= OnMove;
    }

    private void Update()
    {
        IsPlayerGrounded = IsGroundedChecker.IsGrounded();
    
        PlayerInput.UpdateInput();
        UpdateGravitationForce(PlayerGravitation.Gravitate(PlayerVelocity, IsPlayerGrounded, _inspectGravityValue, _passiveStress));
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
        UpdateGravitationForce(PlayerJumper.Jump(this).y);
    }

    public void SetSettings()
    {
        ChangeMoveSpeed(MoveSpeed);
        ChangeCameraSettings(XSensitivity, YSensitivity);
        ChangeJumpingSettings(JumpStartUp, JumpForce);
        
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
    
    private void ChangeJumpingSettings(Vector3 jumpStartUp, float jumpForce)
    {
        JumpForce = jumpForce;
        JumpStartUp = jumpStartUp;

        OnJumpingSettingsChanged?.Invoke(JumpStartUp, JumpForce);
        
        if (_isDebuggingOn)
            Debug.Log($"Jumping setting were changed. Jump start up equals {JumpStartUp}, jump force equals {JumpForce}");
    }
    
    private void UpdateGravitationForce(float gravitationForce)
    {
        PlayerVelocity = new Vector3(PlayerVelocity.x, gravitationForce, PlayerVelocity.z);
    }
    
    private void GetPlayersComponents()
    {
        Camera = GetComponentInChildren<Camera>();
        
        CharacterController = GetComponent<CharacterController>();
        IsGroundedChecker = GetComponent<IsGroundedChecker>();

        PlayerGravitation = new PlayerGravitation(this);
    
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