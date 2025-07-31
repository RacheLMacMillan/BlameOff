using UnityEngine;

public class IsObstacleAboveChecker : BaseSphereChecker
{
    [SerializeField] private LayerMask _toStandLayer;
	[SerializeField] private Vector3 _positionOfCheck;
	[SerializeField] private float _radiusOfCheck;
	
    public bool IsObstaclesAbove()
    {
        return base.CheckArea(_toStandLayer, _positionOfCheck, _radiusOfCheck);
    }

	private void OnDrawGizmos()
    {
		Gizmos.color = Color.green;

		Vector3 drawingPosition = new Vector3
		(
			transform.position.x + _positionOfCheck.x,
			transform.position.y + _positionOfCheck.y,
			transform.position.z + _positionOfCheck.z
		);
    
        Gizmos.DrawWireSphere(drawingPosition, _radiusOfCheck);
    }
}