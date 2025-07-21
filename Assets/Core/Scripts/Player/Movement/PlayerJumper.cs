using System;
using UnityEngine;

public class PlayerJumper : MonoBehaviour, IJumpable<Player>
{
    private CharacterController _characterController;

    [SerializeField] private float _jumpForce;

    [SerializeField] private Vector3 _jumpingStartUp;

    public Vector3 Jump(Player player)
    {
        if (player.IsPlayerGrounded == false)
        {
            throw new ArgumentOutOfRangeException("Player isn't grounded");
        }
        
        transform.position += _jumpingStartUp;

        Vector3 playerVelocity = player.PlayerVelocity;
        
        playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f);

        player.PlayerVelocity = playerVelocity;
        
        _characterController.Move(playerVelocity * Time.deltaTime);

        return Vector3.zero;
    }
}