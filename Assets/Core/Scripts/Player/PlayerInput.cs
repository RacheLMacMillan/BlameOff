using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IInitializable<Player>
{
    private InputMap _inputMap;
    private InputMap.OnFootActions OnFoot;

    private PlayerMover _playerMover;

    public void Initialize(Player player)
    {
        _inputMap = new InputMap();

        SetMaps();

        _playerMover = player.PlayerMover;
        
        OnFoot.Sprint.performed += context => player.OnSprint();
        OnFoot.Crouch.performed += context => player.OnCrouch();
    }

    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();

    private void Update()
    {
        if (OnFoot.Move.ReadValue<Vector2>() != Vector2.zero)
            _playerMover.Move(OnFoot.Move.ReadValue<Vector2>());
        // if (OnFoot.Look.ReadValue<Vector2>() != Vector2.zero)
        //     _playerMover.Move(OnFoot.Look.ReadValue<Vector2>());
    }

    private void SetMaps()
    {
        OnFoot = _inputMap.OnFoot;
    }
}