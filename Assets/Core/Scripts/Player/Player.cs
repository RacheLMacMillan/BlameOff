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
    
    [Header("User settings")]
    [field: SerializeField, Min(0), Range(0, 1)] public float XSensitivity { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float YSensitivity { get; private set; }
    
    [Header("Components")]
    [field: SerializeField] public Camera Camera { get; private set; }
    
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerLooker PlayerLooker { get; private set; }
    [field: SerializeField] public PlayerMover PlayerMover { get; private set; }

    [Header("Do not touch!")]
    [field: SerializeField] public Vector2 PlayerVelocity { get; private set; }

    [Header("Debug settings")]
    [SerializeField] private bool _isDebuggingOn;
    
    // public event Action OnCoreSettingChanged;
    public event Action<float> OnMoveSpeedChanged;
    public event Action<float, float> OnCameraSettingsChanged;
    
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

    public void SetSettings()
    {
        ChangeMoveSpeed(MoveSpeed);
        ChangeCameraSettings(XSensitivity, YSensitivity);
        
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
    
    private void GetPlayersComponents()
    {
        Camera = GetComponentInChildren<Camera>();
        
        CharacterController = GetComponent<CharacterController>();
    
        PlayerInput = GetComponent<PlayerInput>();
        PlayerLooker = GetComponent<PlayerLooker>();
        PlayerMover = GetComponent<PlayerMover>();
        
        if (_isDebuggingOn)
            Debug.Log("The components are gotten.");
    }
    
    private void InitializePlayer()
    {
        PlayerInput.Initialize(this);
        PlayerLooker.Initialize(this);
        PlayerMover.Initialize(this);
        
        if (_isDebuggingOn)
            Debug.Log("The components are initialized.");
    }
}