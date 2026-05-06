using System;
using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    
    [SerializeField] private LayerMask _detectLayers;
    [SerializeField] private Vector3 _position = new Vector3(0, 0.47f, 0);
    [SerializeField] private float _radius = 0.5f;
    
    Vector3 scaledPosition;
    

    private void Update()
    {
        scaledPosition = new Vector3
        (
            transform.position.x + _position.x,
            transform.position.y + _position.y,
            transform.position.z + _position.z
        );
    
        IsGrounded = Physics.CheckSphere(scaledPosition, _radius, _detectLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(scaledPosition, _radius);
    }
}