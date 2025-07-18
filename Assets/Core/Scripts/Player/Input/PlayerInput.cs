using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IInitializable<Player>
{
    [SerializeField] private bool _isDebugging;
    
    private InputMap _inputMap;
    private InputMap.OnFootActions OnFoot;

    public event Action<Vector3> OnPlayerMoving;
    public event Action<Vector2> OnPlayerLooking;
    
    public void Initialize(Player player)
    {
        _inputMap = new InputMap();

        SetMaps();
        
        OnFoot.Sprint.performed += context => player.OnSprint();
        OnFoot.Crouch.performed += context => player.OnCrouch();
        OnFoot.Jump.performed += context => player.OnJump();
    }

    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();

    public void UpdateInput()
    {
        Vector2 lookingDelta = OnFoot.Look.ReadValue<Vector2>();
        Vector2 movingDirection = OnFoot.Move.ReadValue<Vector2>();
    
        if (lookingDelta != Vector2.zero)
        {
            PlayerIsLooking(lookingDelta);
        }

        if (movingDirection != Vector2.zero)
        {
            PlayerIsMoving(movingDirection);
        }
    }
    
    private void PlayerIsMoving(Vector2 direction)
    {
        Vector3 NormalizedDirection = NormalizeDirection(direction);
            
        OnPlayerMoving?.Invoke(NormalizedDirection);
        
        if (_isDebugging)
            Debug.Log($"Player is moving with delta {NormalizedDirection}");
    }

    private void PlayerIsLooking(Vector2 delta)
    {
        OnPlayerLooking?.Invoke(delta);
        if (_isDebugging)
            Debug.Log($"Player is looking with delta {delta}");
    }

    private void SetMaps()
    {
        OnFoot = _inputMap.OnFoot;
    }
    
    private Vector3 NormalizeDirection(Vector2 direction)
    {
        return new Vector3(direction.x, 0, direction.y);
    }
}