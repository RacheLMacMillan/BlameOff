using UnityEngine;

namespace FPS
{
	public abstract class BaseGravitation
	{
		public float InspectGravityValue { get; private set; }
		public float AdditionalInspectGravityValue { get; private set; }
		public float PassiveStress  { get; private set; }

		protected BaseGravitation(float inspectGravityValue, float additionalInspectGravityValue, float passiveStress)
		{
			InspectGravityValue = inspectGravityValue;
			AdditionalInspectGravityValue = additionalInspectGravityValue;
			PassiveStress = passiveStress;
		}
		
		protected BaseGravitation()
		{
			InspectGravityValue = -10;
			AdditionalInspectGravityValue = -15;
			PassiveStress = -2;
		}
	
		public virtual float BaseGravitate(float velocity, bool isGrounded)
		{
			if (isGrounded)
				velocity = PassiveStress;
			else if (velocity > 0)
				velocity += AdditionalInspectGravityValue * Time.deltaTime;
			else
				velocity += InspectGravityValue * Time.deltaTime;
		
			return velocity;
		}
	}
}