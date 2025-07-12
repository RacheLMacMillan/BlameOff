using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerLooker))]
public class Player : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebuggingOn;

    [Header("Core settings")]
    [field: SerializeField] public float Health { get; private set; }
    
    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Min(1), Range(1, 10)] public float SpringSpeed { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float CrouchSpeed { get; private set; }
    
    [Header("Components")]
    
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerMover PlayerMover { get; private set; }
    [field: SerializeField] public PlayerLooker PlayerLooker { get; private set; }

    [Header("Do not touch!")]
    [field: SerializeField] public Vector2 PlayerVelocity { get; private set; }
    
    private void Awake()
    {
        GetPlayersComponents();
        InitializePlayer();
    }

    private void OnEnable()
    {
        PlayerMover.OnMoveSpeedChanged += OnSpeedMoveChanged;
    }
    
    private void OnDisable()
    {
        PlayerMover.OnMoveSpeedChanged -= OnSpeedMoveChanged;
    }

    public void OnSprint() {}
    public void OnCrouch() {}
    public void OnLook() {}
    
    private void OnSpeedMoveChanged(float value)
    {
        MoveSpeed = value;
        
        if (_isDebuggingOn)
            Debug.Log(MoveSpeed);
    }
    
    private void GetPlayersComponents()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMover = GetComponent<PlayerMover>();
        PlayerLooker = GetComponent<PlayerLooker>();
        
        if (_isDebuggingOn)
            Debug.Log("The components are gotten");
    }
    
    private void InitializePlayer()
    {
        PlayerInput.Initialize(this);
        PlayerMover.Initialize(this);
        PlayerLooker.Initialize(this);
        
        if (_isDebuggingOn)
            Debug.Log("The components are initialized");
    }
}