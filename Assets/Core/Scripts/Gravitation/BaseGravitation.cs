using UnityEngine;

public abstract class BaseGravitation
{
    protected float _inspectGravityValue { get; private set; }
	protected float _passiveStress { get; private set; }
	
	public BaseGravitation(float inspectGravityValue, float passiveStress)
	{
		_inspectGravityValue = inspectGravityValue;
		_passiveStress = passiveStress;
	}
	
	public virtual float BaseGravitate(float velocity, bool isGrounded)
	{
		velocity += _inspectGravityValue * Time.deltaTime;
		
		if (isGrounded == true)
		{
			velocity = _passiveStress;
		}
		
		Debug.Log("Gravitated");
		
		return velocity;
	}
}