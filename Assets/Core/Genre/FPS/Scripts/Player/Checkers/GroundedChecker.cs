using UnityEngine;

namespace FPS
{
    public class GroundedChecker : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }
    
        [SerializeField] private LayerMask _detectLayers;
        [SerializeField] private Vector3 _position = new Vector3(0, 0.47f, 0);
        [SerializeField] private float _radius = 0.5f;

        [SerializeField] private bool _isDebugging;
    
        private void Update()
        {
            IsGrounded = Physics.CheckSphere(ScalePosition(_position), _radius, _detectLayers);
            
            if (_isDebugging)
                Debug.Log("Is player grounded: " + IsGrounded);
        }

        private void OnDrawGizmos()
        {
            if (_isDebugging)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(ScalePosition(_position), _radius);
            }
        }
    
        private Vector3 ScalePosition(Vector3 position)
        {
            return new Vector3
            (
                transform.position.x + position.x,
                transform.position.y + position.y,
                transform.position.z + position.z
            );
        }
    }
}