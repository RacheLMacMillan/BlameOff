using UnityEngine;

public class Gravitation
{
    [SerializeField] private float _inspectGravityValue = -9.8f;
	[SerializeField] private float _passiveStress = -2;
	
	public Gravitation() {  }
	
	public Vector3 GravitatePlayer(Vector3 velocity, bool isGrounded)
	{
        // isGrounded = true;
        
		velocity.y += _inspectGravityValue * Time.deltaTime;
		
		// if (isGrounded == true)
		// {
		// 	velocity.y = _passiveStress;
		// }
		
		return new Vector3(0, velocity.y, 0);
	}
}