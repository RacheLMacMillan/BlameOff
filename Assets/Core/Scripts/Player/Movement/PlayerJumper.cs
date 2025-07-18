using System;
using UnityEngine;

public class PlayerJumper : IJumpable<Vector3, bool>
{
    private CharacterController _characterController;

    private float _jumpForce;

    public PlayerJumper(CharacterController characterController, float jumpForce)
    {
        _jumpForce = jumpForce;

        _characterController = characterController;
    }

    public Vector3 Jump(Vector3 playerVelocity, bool isGrounded)
    {
        if (isGrounded == false)
        {
            throw new ArgumentOutOfRangeException("Player isn't grounded");
        }

        playerVelocity.y = Mathf.Sqrt(-_jumpForce * -9.8f) * Time.deltaTime;
        
        _characterController.Move(playerVelocity);

        return playerVelocity * Time.deltaTime;
    }
}