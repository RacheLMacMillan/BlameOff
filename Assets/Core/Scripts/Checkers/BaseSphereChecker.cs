using UnityEngine;

public abstract class BaseSphereChecker : MonoBehaviour
{
	public virtual bool CheckArea(LayerMask toStandLayer, Vector3 positionOfCheck, float radiusOfCheck)
	{
	    return Physics.CheckSphere
        (
            SumPosition(transform.position, positionOfCheck),
            radiusOfCheck,
            toStandLayer
        );
	}

    private Vector3 SumPosition(Vector3 center, Vector3 checkingPosition)
	{
		return new Vector3
		(
			transform.localPosition.x + checkingPosition.x,
			transform.localPosition.y + checkingPosition.y,
			transform.localPosition.z + checkingPosition.z
		);
	}
}