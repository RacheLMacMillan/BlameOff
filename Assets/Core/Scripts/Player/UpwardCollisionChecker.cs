using System;
using UnityEngine;

namespace Core.Scripts.Player
{
    public class UpwardCollisionChecker : MonoBehaviour
    {
        public Action<bool> OnHitFromAbove;
        
        private bool _isHitFromAbove;
    
        [SerializeField] private LayerMask _detectLayers;
        [SerializeField] private Vector3 _position = new Vector3(0, 1.52f, 0);
        [SerializeField] private float _radius = 0.5f;
    
        private void Update()
        {
            bool newCheckSphere = Physics.CheckSphere(ScalePosition(_position), _radius, _detectLayers);

            if (newCheckSphere != _isHitFromAbove)
            {
                _isHitFromAbove = newCheckSphere;
                OnHitFromAbove?.Invoke(_isHitFromAbove);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(ScalePosition(_position), _radius);
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
