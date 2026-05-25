using System;
using UnityEngine;

namespace Core.Scripts.Player.Checkers
{
    public class IsAbleToStandUpChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _detectLayers;
        [SerializeField] private Vector3 _checkingPosition;
        [SerializeField] private float _radius;
        
        public bool IsPlayerAbleToStandUp { get; private set; }

        private void Update()
        {
            IsPlayerAbleToStandUp = Physics.CheckSphere(ScalePosition(_checkingPosition), _radius, _detectLayers);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ScalePosition(_checkingPosition), _radius);
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
