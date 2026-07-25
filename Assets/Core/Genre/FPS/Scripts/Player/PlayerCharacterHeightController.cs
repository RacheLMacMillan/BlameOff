using System;
using UnityEngine;

namespace FPS
{
    public class PlayerCharacterHeightController : MonoBehaviour
    {
        [Header("Stand state")]
        [field: SerializeField] public float StandHeight { get; private set; }
        [field: SerializeField] public Vector3 StandCenter { get; private set; }

        public Action OnPlayerSandedUp;

        public bool IsStanding { get; private set; } = true;
        public bool IsCrouching { get; private set; }
        public bool IsLayingDown { get; private set; }
        
        private CharacterController _characterController;
        private PlayerCrouch _playerCrouch;
        private PlayerLieDowner _playerLieDowner;
        private IsAbleToStandUpChecker _isAbleToStandUpChecker;

        private PlayerJumper _playerJumper;
        
        [SerializeField] private bool _isDebugging = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
            _playerLieDowner = GetComponent<PlayerLieDowner>();
            _isAbleToStandUpChecker = GetComponent<IsAbleToStandUpChecker>();
            
            _playerJumper = GetComponent<PlayerJumper>();
        }

        private void OnEnable()
        {
            _playerCrouch.OnPlayerCrouched += ChangeToCrouch;
            _playerLieDowner.OnPlayerLayDown += ChangeToLayingDown;
            _playerJumper.OnPlayerNeedToStandUp += ChangeToStand;
        }

        private void OnDisable()
        {
            _playerCrouch.OnPlayerCrouched -= ChangeToCrouch;
            _playerLieDowner.OnPlayerLayDown -= ChangeToLayingDown;
            _playerJumper.OnPlayerNeedToStandUp -= ChangeToStand;
        }
        
        private void ChangeToStand()
        {
            if (!_isAbleToStandUpChecker.IsPlayerAbleToStandUp)
                throw new ArgumentOutOfRangeException("Player isn't able to stand up");
            
            IsStanding = true;
            IsCrouching = false;
            IsLayingDown = false;
            
            _characterController.height = StandHeight;
            _characterController.center = StandCenter;
            
            OnPlayerSandedUp?.Invoke();

            if (_isDebugging)
                Debug.Log("Changed to stand");
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
    }
}