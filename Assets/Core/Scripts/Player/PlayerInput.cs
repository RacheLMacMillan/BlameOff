using UnityEngine;

public class PlayerInput : MonoBehaviour, IInitializable
{
    private InputMap _inputMap;
    private InputMap.OnFootActions OnFoot;

    private PlayerMover _playerMover;

    public void Initialize()
    {
        _inputMap = new InputMap();

        SetMaps();

        Player player = GetComponent<Player>();

        _playerMover = player.PlayerMover;
        
        OnFoot.Sprint.performed += context => player.Sprint();
        OnFoot.Crouch.performed += context => player.Crouch();
    }

    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();

    private void Update()
    {
    
        if (OnFoot.Move.ReadValue<Vector2>() != Vector2.zero)
            _playerMover.Move(OnFoot.Move.ReadValue<Vector2>());
    }

    private void SetMaps()
    {
        OnFoot = _inputMap.OnFoot;
    }
}