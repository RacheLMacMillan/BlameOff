using System;
using Core.Scripts.Player.Movement;
using UnityEngine;

namespace Core.Scripts.Player.Checkers
{
    public class UpwardCollisionChecker : MonoBehaviour
    {
        [Header("Default values")]
        [SerializeField] private LayerMask _detectLayers;
        [SerializeField] private Vector3 _position = new Vector3(0, 1.52f, 0);
        [SerializeField] private float _radius = 0.5f;
        
        [Header("Body positions")]
        [SerializeField] private Vector3 _standingPosition;
        [SerializeField] private Vector3 _crouchingPosition;
        [SerializeField] private Vector3 _layingPosition;
    
        public Action<bool> OnHitFromAbove;
        
        private PlayerCharacterHeightController _playerCharacterHeightController;
        private PlayerCrouch  _playerCrouch;
        private PlayerLieDowner _playerLieDowner;
        
        private bool _isHitFromAbove;
        private bool _isDebugging;
        
        private void OnEnable()
        {
            _playerCharacterHeightController.OnPlayerSandedUp += StandUp;
            _playerCrouch.OnPlayerCrouched += Crouch;
            _playerLieDowner.OnPlayerLayDown += LieDown;
        }
        
        private void OnDisable()
        {
            _playerCharacterHeightController.OnPlayerSandedUp -= StandUp;
            _playerCrouch.OnPlayerCrouched -= Crouch;
            _playerLieDowner.OnPlayerLayDown -= LieDown;
        }

        private void Awake()
        {
            _playerCharacterHeightController = GetComponent<PlayerCharacterHeightController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
            _playerLieDowner = GetComponent<PlayerLieDowner>();
        }

        private void Update()
        {
            bool newCheckSphere = Physics.CheckSphere(ScalePosition(_position), _radius, _detectLayers);

            if (newCheckSphere != _isHitFromAbove)
            {
                _isHitFromAbove = newCheckSphere;
                OnHitFromAbove?.Invoke(_isHitFromAbove);

                if (_isDebugging)
                    Debug.Log("Player was hit from above");
            }
        }

        private void StandUp()
        {
            _position = _standingPosition;
        }

        private void Crouch()
        {
            _position = _crouchingPosition;
        }

        private void LieDown()
        {
            _position = _layingPosition;
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
