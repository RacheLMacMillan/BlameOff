using UnityEngine;

public abstract class Gravitation
{
    public virtual void Gravitate(Vector3 velocity, bool isGrounded, float inspectGravityValue, float passiveStress)
    {
        
        if (isGrounded == true)
        {
            velocity.y = passiveStress;
        }
        else 
        {
            velocity.y += inspectGravityValue * Time.deltaTime;
        }

        // return velocity;
    }
}