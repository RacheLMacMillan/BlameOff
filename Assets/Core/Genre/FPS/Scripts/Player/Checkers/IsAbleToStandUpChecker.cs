using UnityEngine;

namespace FPS
{
    public class IsAbleToStandUpChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _detectLayers;
        [SerializeField] private Vector3 _checkingPosition;
        [SerializeField] private float _radius;
        
        [SerializeField] private bool _isDebugging;
        
        
        public bool IsPlayerAbleToStandUp { get; private set; }

        private void Update()
        {
            IsPlayerAbleToStandUp = !Physics.CheckSphere(ScalePosition(_checkingPosition), _radius, _detectLayers);
            
            if (_isDebugging)
                Debug.Log("Is player able to stand up: " + IsPlayerAbleToStandUp);
        }

        private void OnDrawGizmos()
        {
            if (_isDebugging)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(ScalePosition(_checkingPosition), _radius);
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
