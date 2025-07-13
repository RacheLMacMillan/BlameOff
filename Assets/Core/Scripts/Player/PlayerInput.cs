using UnityEngine;

public class PlayerInput : MonoBehaviour, IInitializable<Player>
{
    private InputMap _inputMap;
    private InputMap.OnFootActions OnFoot;

    private Player _player;

    public void Initialize(Player player)
    {
        _inputMap = new InputMap();

        _player = player;

        SetMaps();
        
        OnFoot.Sprint.performed += context => _player.OnSprint();
        OnFoot.Crouch.performed += context => _player.OnCrouch();
    }

    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();

    private void Update()
    {
        if (OnFoot.Look.ReadValue<Vector2>() != Vector2.zero)
            _player.OnLook(OnFoot.Look.ReadValue<Vector2>());

        if (OnFoot.Move.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector3 NormalizedDirection = NormalizeDirection(OnFoot.Move.ReadValue<Vector2>());
            
            _player.OnMove(NormalizedDirection);
        }
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