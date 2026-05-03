using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector3> OnPlayerMove;
    public Action<Vector2> OnPlayerLook;

    private InputMap _inputMap;
    private InputMap.OnFootActions _onFoot;
    
    private PlayerMover _playerMover;

    private void Awake()
    {
        _inputMap = new InputMap();
        _onFoot = _inputMap.OnFoot;
        
        _onFoot.Reload.canceled += context => Debug.Log("Reload");
        _onFoot.Reload.performed += context => Debug.Log("Check magazine");
    }

    private void Update()
    {
        if (_onFoot.Move.ReadValue<Vector2>() != Vector2.zero)
            OnPlayerMove?.Invoke(ScaleDirection(_onFoot.Move.ReadValue<Vector2>()));
        if (_onFoot.Look.ReadValue<Vector2>() != Vector2.zero)
            OnPlayerLook?.Invoke(_onFoot.Look.ReadValue<Vector2>());
    }
    
    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();
    
    private Vector3 ScaleDirection(Vector2 direction)
    {
        return new Vector3(direction.x, 0, direction.y);
    }
    
    
}