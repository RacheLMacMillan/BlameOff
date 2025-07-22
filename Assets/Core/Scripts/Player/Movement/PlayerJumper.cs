using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IInitializable<Player>, IJumpable<Player>
{
    [SerializeField] private float _jumpForce;

    [SerializeField] private Vector3 _jumpStartUp;
    
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
        
        _jumpStartUp = player.JumpStartUp;
        _jumpForce = player.JumpForce;
    }

    private void OnEnable() => _player.OnJumpingSettingsChanged += ChangeSettings;
    private void OnDisable() => _player.OnJumpingSettingsChanged -= ChangeSettings;

    public Vector3 Jump(Player player)
    {
        if (player.IsPlayerGrounded == false)
        {
            throw new ArgumentOutOfRangeException("Player isn't grounded");
        }
        
        transform.position += _jumpStartUp;

        Vector3 playerVelocity = player.PlayerVelocity;
        
        playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f);

        return playerVelocity;
    }
    
    private void ChangeSettings(Vector3 jumpStartUp, float jumpForce)
    {
        _jumpStartUp = jumpStartUp;
        _jumpForce = jumpForce;
    }
}