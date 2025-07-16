using UnityEngine;

public abstract class Gravitation
{
    public virtual float Gravitate(Vector3 velocity, bool isGrounded, float inspectGravityValue, float passiveStress)
    {
        float gravitationForce = velocity.y;
    
        if (isGrounded == true)
        {
            gravitationForce = passiveStress;
        }
        else 
        {
            gravitationForce += inspectGravityValue * Time.deltaTime;
        }

        return gravitationForce;
    }
}