using System;
using UnityEngine;
using Core.Scripts.Player.Movement;

namespace Core.Scripts.Player
{
    public class PlayerCharacterHeightController : MonoBehaviour
    {
        private CharacterController _characterController;
        private PlayerCrouch _playerCrouch;
        
        [SerializeField] private bool _isDebugging = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
        }

        private void OnEnable()
        {
            _playerCrouch.OnPlayerCrouched += ChangeToCrouch;
            _playerCrouch.OnPlayerSandedUp += ChangeToStand;
        }

        private void OnDisable()
        {
            _playerCrouch.OnPlayerCrouched -= ChangeToCrouch;
            _playerCrouch.OnPlayerSandedUp -= ChangeToStand;
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