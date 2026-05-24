using System;
using UnityEngine;
using Core.Scripts.Player.Movement;

namespace Core.Scripts.Player
{
    public class PlayerCharacterHeightController : MonoBehaviour
    {
        [Header("Stand state")]
        [field: SerializeField] public float StandHeight { get; private set; }
        [field: SerializeField] public Vector3 StandCenter { get; private set; }

        public Action OnPlayerSandedUp;
        
        public bool IsStanding { get; private set; }
        public bool IsCrouching { get; private set; }
        public bool IsLayingDown { get; private set; }
        
        private CharacterController _characterController;
        private PlayerCrouch _playerCrouch;
        private PlayerLieDowner _playerLieDowner;
        
        [SerializeField] private bool _isDebugging = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
            _playerLieDowner = GetComponent<PlayerLieDowner>();
        }

        private void OnEnable()
        {
            _playerCrouch.OnPlayerCrouched += ChangeToCrouch;
            _playerLieDowner.OnPlayerLayDown += ChangeToLayingDown;
        }

        private void OnDisable()
        {
            _playerCrouch.OnPlayerCrouched -= ChangeToCrouch;
            _playerLieDowner.OnPlayerLayDown -= ChangeToLayingDown;
        }
        
        private void ChangeToStand()
        {
            IsStanding = true;
            IsCrouching = false;
            IsLayingDown = false;
            
            _characterController.height = StandHeight;
            _characterController.center = StandCenter;
            
            OnPlayerSandedUp?.Invoke();

            if (_isDebugging)
                Debug.Log("Changed to stand");
        }
        
        private void ChangeToLayingDown()
        {
            if (IsLayingDown)
            {
                ChangeToStand();
                return;
            }
            
            IsStanding = false;
            IsCrouching = false;
            IsLayingDown = true;
            
            _characterController.height = _playerLieDowner.LayingHeight;
            _characterController.center = _playerLieDowner.LayingCenter;

            if (_isDebugging)
                Debug.Log("Changed to laying down");
        }

        private void ChangeToCrouch()
        {
            if (IsCrouching)
            {
                ChangeToStand();
                return;
            }
            
            IsStanding = false;
            IsCrouching = true;
            IsLayingDown = false;
            
            _characterController.height = _playerCrouch.CrouchingHeight;
            _characterController.center = _playerCrouch.CrouchingCenter;

            if (_isDebugging)
                Debug.Log("Changed to crouch");
        }
    }
}