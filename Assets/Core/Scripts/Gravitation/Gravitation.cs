using UnityEngine;

public abstract class Gravitation
{
    protected float _inspectGravityValue { get; private set; }
	protected float _passiveStress { get; private set; }
	
	public Gravitation(float inspectGravityValue, float passiveStress)
	{
		_inspectGravityValue = inspectGravityValue;
		_passiveStress = passiveStress;
	}
	
	public virtual float GravitatePlayer(float velocity, bool isGrounded)
	{
		velocity += _inspectGravityValue * Time.deltaTime;
		
		// if (isGrounded == true)
		// {
		// 	velocity = _passiveStress;
		// }
		
		return velocity;
	}
}