using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IInitializable<Player>, IMoveable
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    public event Action<float> OnMoveSpeedChanged;
    
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
        MoveSpeed = _player.MoveSpeed;
    }

    public void Move(Vector2 direction)
    {
        Debug.Log("Move: " + direction);
    }
    
    public void SetSettings()
    {
        OnMoveSpeedChanged?.Invoke(MoveSpeed);
        Debug.Log("Settings were set");
    }
}