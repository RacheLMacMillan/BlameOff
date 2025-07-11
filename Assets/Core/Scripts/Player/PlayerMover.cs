using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IInitializable, IMoveable
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    public event Action<float> OnMoveSpeedChanged;
    
    private Player _player;

    public void Initialize()
    {
        _player = GetComponent<Player>();
    }

    public void Move(Vector2 direction)
    {
        Debug.Log("Move");
    }
    
    public void SetSettings()
    {
        OnMoveSpeedChanged?.Invoke(MoveSpeed);
        Debug.Log("Settings were set");
    }
}