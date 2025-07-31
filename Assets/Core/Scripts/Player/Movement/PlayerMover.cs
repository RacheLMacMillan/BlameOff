using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IInitializable<Player>, IMoveable<Vector3>
{
    private float MoveSpeed;

    public event Action<float> OnMoveSpeedChanged;
    
    private Player _player;

    private void OnEnable() => _player.OnMoveSpeedChanged += SetSetting;
    private void OnDisable() => _player.OnMoveSpeedChanged -= SetSetting;

    public void Initialize(Player player)
    {
        _player = player;
        
        MoveSpeed = _player.MoveSpeed;
    }

    public void Move(Vector3 direction)
    {
        float scaledMoveSpeed = MoveSpeed * Time.deltaTime;

        Vector3 scaledDirection = new Vector3
        (
            direction.x * scaledMoveSpeed,
            direction.y * scaledMoveSpeed,
            direction.z * scaledMoveSpeed
        );

        _player.CharacterController.Move(transform.TransformDirection(scaledDirection));
    }
    
    private void SetSetting(float speed)
    {
        MoveSpeed = speed;

        OnMoveSpeedChanged?.Invoke(speed);
    }
}