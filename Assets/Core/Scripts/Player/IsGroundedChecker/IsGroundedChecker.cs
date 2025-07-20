using UnityEngine;

public class IsGroundedChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _toStandLayer;
	[SerializeField] private Vector3 _positionOfCheck;
	[SerializeField] private float _radiusOfCheck;

    public bool IsGrounded()
    {
        return Physics.CheckSphere
        (
            ScalePosition(),
            _radiusOfCheck,
            _toStandLayer
        );
    }
    
    private Vector3 ScalePosition()
	{
		return new Vector3
		(
			transform.localPosition.x + _positionOfCheck.x, 
			transform.localPosition.y + _positionOfCheck.y, 
			transform.localPosition.z + _positionOfCheck.z
		);
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ScalePosition(), _radiusOfCheck);
    }
}