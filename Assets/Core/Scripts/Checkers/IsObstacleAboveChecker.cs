using UnityEngine;

public class IsObstacleAboveChecker : MonoBehaviour, IInitializable<PositionSummer>
{
    private PositionSummer _positionSummer;

    public void Initialize(PositionSummer positionSummer)
    {
        _positionSummer = positionSummer;
    }
    
    public void IsObstaclesAbove()
    {
        // return Physics.CheckSphere
        // (
        //     _positionSummer.SumPosition(transform.position, _positionOfCheck),
        //     _radiusOfCheck,
        //     _toStandLayer
        // );
    }
}