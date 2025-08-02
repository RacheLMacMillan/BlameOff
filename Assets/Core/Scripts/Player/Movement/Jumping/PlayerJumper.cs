using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IInitializable<Player>, IJumpable<Vector3, bool, bool>
{
    [SerializeField] private Vector3 _jumpStartUp;

    private float _jumpForce;
    
    private Player _player;

    public event Action OnJumped;

    public void Initialize(Player player)
    {
        _player = player;
        
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
        if (isObstacleAbove == true)
        {
            throw new ArgumentOutOfRangeException("There is something from above.");
        }
        
        transform.position += _jumpStartUp;
        
        playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f);

        OnJumped?.Invoke();

        return playerVelocity;
    }
    
    private void ChangeSettings(float jumpForce)
    {
        _jumpForce = jumpForce;
    }
}