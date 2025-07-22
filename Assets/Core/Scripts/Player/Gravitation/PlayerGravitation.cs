using UnityEngine;

public class PlayerGravitation : Gravitation
{
    private CharacterController _playerController;

    public PlayerGravitation(CharacterController playerController)
    {
        _playerController = playerController;
    }

    public override float Gravitate(Vector3 velocity, bool isGrounded, float inspectGravityValue, float passiveStress)
    {
        float gravitationForce = base.Gravitate(velocity, isGrounded, inspectGravityValue, passiveStress);

        Vector3 gravitationDirection = new Vector3(0, gravitationForce * Time.deltaTime, 0);
        
        _playerController.Move(gravitationDirection);
        
        return gravitationForce;
    }
}