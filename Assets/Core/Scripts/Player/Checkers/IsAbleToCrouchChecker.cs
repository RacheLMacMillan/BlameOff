using System;
using UnityEngine;

namespace Core.Scripts.Player.Checkers
{
    public class IsAbleToCrouchChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _detectLayers;
        [SerializeField] private Vector3 _checkingPosition;
        [SerializeField] private float _radius;

        [SerializeField] private bool _isDebugging;
        
        public bool IsPlayerAbleToCrouch { get; private set; }

        private void Update()
        { 
            // Why there's "!"
            // Physics.CheckSphere checks collision with chosen layers
            // But we want to check that there's nothing of chosen layers
            // So I put "!"
            IsPlayerAbleToCrouch = !Physics.CheckSphere(ScalePosition(_checkingPosition), _radius, _detectLayers);

            if (_isDebugging)
                Debug.Log("Is player able to crouch: " + IsPlayerAbleToCrouch);
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
