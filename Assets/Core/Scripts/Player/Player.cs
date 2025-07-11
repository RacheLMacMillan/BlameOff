using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private bool _isDebuggingOn;

    public float Health { get; private set; }
    
    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; }
    [field: SerializeField, Min(1), Range(1, 10)] public float SpringSpeed { get; private set; }
    [field: SerializeField, Min(0), Range(0, 1)] public float CrouchSpeed { get; private set; }
    
    public Vector2 PlayerVelocity { get; private set; }
    
    public PlayerInput PlayerInput { get; private set; }
    
    public PlayerMover PlayerMover { get; private set; }

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

    public void Sprint() {}
    public void Crouch() {}
    public void Look() {}
    
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
    }
    
    private void InitializePlayer()
    {
        PlayerInput.Initialize();
        PlayerMover.Initialize();
    }
}