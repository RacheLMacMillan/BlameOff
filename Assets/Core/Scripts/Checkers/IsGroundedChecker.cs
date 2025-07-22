using UnityEngine;

public class IsGroundedChecker : MonoBehaviour, IInitializable<PositionSummer>
{
    [SerializeField] private LayerMask _toStandLayer;
	[SerializeField] private Vector3 _positionOfCheck;
	[SerializeField] private float _radiusOfCheck;
	
	private PositionSummer _positionSummer;
	
	public void Initialize(PositionSummer positionSummer)
	{
	    _positionSummer = positionSummer;
	}

    public bool IsGrounded()
    {
        return Physics.CheckSphere
        (
            _positionSummer.SumPosition(transform.position, _positionOfCheck),
            _radiusOfCheck,
            _toStandLayer
        );
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(_positionSummer.SumPosition(transform.position, _positionOfCheck), _radiusOfCheck);
    // }
}