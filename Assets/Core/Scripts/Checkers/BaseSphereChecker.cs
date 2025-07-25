using UnityEngine;

public abstract class BaseSphereChecker : MonoBehaviour
{


    private virtual Vector3 SumPosition(Vector3 center, Vector3 checkingPosition)
	{
		return new Vector3
		(
			transform.localPosition.x + checkingPosition.x,
			transform.localPosition.y + checkingPosition.y,
			transform.localPosition.z + checkingPosition.z
		);
	}
	
	private abstract void OnDrawGizmos() {
        // dsfafsfds
    }
}