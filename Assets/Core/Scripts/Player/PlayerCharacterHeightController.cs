using System;
using Core.Scripts.Player.Movement;
using UnityEngine;

namespace Core.Scripts.Player
{
    public class PlayerCharacterHeightController : MonoBehaviour
    {
        private CharacterController _characterController;
        private PlayerCrouch _playerCrouch;
        
        private bool _isCrouching;
        
        private float _normalHeight = 1;
        private Vector3 _normalCenter = new Vector3(0, 1, 0);

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
        }

        private void OnEnable() => _playerCrouch.OnPlayerCrouched += Crouch;
        private void OnDisable() => _playerCrouch.OnPlayerCrouched -= Crouch;

        private void Crouch()
        {
            if (!_isCrouching)
            {
                _characterController.height = Mathf.Lerp(_normalHeight, _normalHeight * _playerCrouch.CrouchHeight, _playerCrouch.CrouchSpeed * Time.deltaTime);
                _characterController.center = new Vector3(0, Mathf.Lerp(_normalCenter.y, _normalCenter.y * _playerCrouch.CrouchHeight, _playerCrouch.CrouchSpeed * Time.deltaTime), 0);
                
                _isCrouching = true;
            }
            else
            {
                _characterController.height = Mathf.Lerp(_normalHeight * _playerCrouch.CrouchHeight, _normalHeight, _playerCrouch.CrouchSpeed * Time.deltaTime);
                _characterController.center = new Vector3(0, Mathf.Lerp(_normalCenter.y * _playerCrouch.CrouchHeight, _normalCenter.y, _playerCrouch.CrouchSpeed * Time.deltaTime), 0);
                
                _isCrouching = false;
            }
        }
    }
}