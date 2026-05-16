using UnityEngine;

namespace Core.Scripts.Gravitation
{
	public abstract class BaseGravitation
	{
		private float InspectGravityValue { get; set; }
		private float PassiveStress { get; set; }

		protected BaseGravitation(float inspectGravityValue, float passiveStress)
		{
			InspectGravityValue = inspectGravityValue;
			PassiveStress = passiveStress;
		}
	
		public virtual float BaseGravitate(float velocity, bool isGrounded)
		{
			velocity += InspectGravityValue * Time.deltaTime;
		
			if (isGrounded == true)
			{
				velocity = PassiveStress;
			}
		
			return velocity;
		}
	}
}