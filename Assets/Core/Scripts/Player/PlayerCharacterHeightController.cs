using UnityEngine;
using Core.Scripts.Player.Movement;

namespace Core.Scripts.Player
{
    public class PlayerCharacterHeightController : MonoBehaviour
    {
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
            _playerCrouch.OnPlayerSandedUp += ChangeToStand;
            _playerLieDowner.OnPlayerLayDown += ChangeToLayingDown;
        }

        private void OnDisable()
        {
            _playerCrouch.OnPlayerCrouched -= ChangeToCrouch;
            _playerCrouch.OnPlayerSandedUp -= ChangeToStand;
            _playerLieDowner.OnPlayerLayDown -= ChangeToLayingDown;
        }

        private void ChangeToLayingDown()
        {
            _characterController.height = _playerLieDowner.LayingHeight;
            _characterController.center = _playerLieDowner.LayingCenter;

            if (_isDebugging)
                Debug.Log("Changed to laying down");
        }

        private void ChangeToCrouch()
        {
            _characterController.height = _playerCrouch.CrouchingHeight;
            _characterController.center = _playerCrouch.CrouchingCenter;

            if (_isDebugging)
                Debug.Log("Changed to crouch");
        }
        
        private void ChangeToStand()
        {
            _characterController.height = _playerCrouch.StandHeight;
            _characterController.center = _playerCrouch.StandCenter;

            if (_isDebugging)
                Debug.Log("Changed to stand");
        }
    }
}