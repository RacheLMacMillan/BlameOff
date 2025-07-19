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
            new Vector3(transform.position.x, transform.position.y + 0.45f, transform.position.z),
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
}