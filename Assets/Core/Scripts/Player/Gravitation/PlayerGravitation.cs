using UnityEngine;

public class PlayerGravitation : Gravitation
{
    private Player _player;

    public PlayerGravitation(Player player)
    {
        _player = player;
    }

    public override void Gravitate(Vector3 velocity, bool isGrounded, float inspectGravityValue, float passiveStress)
    {
        
        
        base.Gravitate(velocity, isGrounded, inspectGravityValue, passiveStress);
    }
}