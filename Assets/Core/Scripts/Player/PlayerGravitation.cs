using UnityEngine;

public class PlayerGravitation : Gravitation
{
    public PlayerGravitation(float inspectGravityValue, float passiveStress)
        : base(inspectGravityValue, passiveStress) {  }

    public override float GravitatePlayer(float velocity, bool isGrounded)
    {
        return base.GravitatePlayer(velocity, isGrounded);
    }
}