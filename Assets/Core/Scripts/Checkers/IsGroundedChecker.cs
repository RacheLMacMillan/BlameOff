using UnityEngine;

public class IsGroundedChecker : BaseSphereChecker
{
    [SerializeField] private LayerMask _toStandLayer;
	[SerializeField] private Vector3 _positionOfCheck;
	[SerializeField] private float _radiusOfCheck;
	
	public bool IsGrounded()
	{
        return base.CheckArea(_toStandLayer, _positionOfCheck, _radiusOfCheck);
	}
	
	private void OnDrawGizmos()
    {
		Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_positionOfCheck, _radiusOfCheck);
    }
}