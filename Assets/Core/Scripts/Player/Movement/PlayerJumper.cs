using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IInitializable<Player>, IJumpable<Vector3, bool, bool>
{
    private float _jumpForce;

    private Vector3 _jumpStartUp;
    
    private Player _player;

    public event Action OnJumped;

    public void Initialize(Player player)
    {
        _player = player;
        
        _jumpStartUp = player.JumpStartUp;
        _jumpForce = player.JumpForce;
    }

    private void OnEnable() => _player.OnJumpingSettingsChanged += ChangeSettings;
    private void OnDisable() => _player.OnJumpingSettingsChanged -= ChangeSettings;

    public Vector3 Jump(Vector3 playerVelocity, bool isGrounded, bool isObstacleAbove)
    {
        if (isGrounded == false)
        {
            throw new ArgumentOutOfRangeException("Player isn't grounded.");
        }
        if (isObstacleAbove == false)
        {
            throw new ArgumentOutOfRangeException("There is something from above.");
        }
        
        transform.position += _jumpStartUp;
        
        playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f);

        OnJumped?.Invoke();

        return playerVelocity;
    }
    
    private void ChangeSettings(Vector3 jumpStartUp, float jumpForce)
    {
        _jumpStartUp = jumpStartUp;
        _jumpForce = jumpForce;
    }
}