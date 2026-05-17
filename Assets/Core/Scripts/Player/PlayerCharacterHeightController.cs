using System;
using Core.Scripts.Player.Movement;
using UnityEngine;

namespace Core.Scripts.Player
{
    public class PlayerCharacterHeightController : MonoBehaviour
    {
        public Action<bool> OnPlayerCrouched;
        
        private CharacterController _characterController;
        private PlayerCrouch _playerCrouch;
        private Transform _camera;
        
        private bool _isCrouching;

        private readonly float _normalHeight = 2;
        private readonly Vector3 _normalCenter = new Vector3(0, 1, 0);
        private readonly Vector3 _cameraPosition = new Vector3(0, 1.75f, 0);

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
            _camera = GetComponentInChildren<Camera>().transform;
        }

        private void OnEnable() => _playerCrouch.OnPlayerCrouched += SwitchCrouching;
        private void OnDisable() => _playerCrouch.OnPlayerCrouched -= SwitchCrouching;

        private void SwitchCrouching()
        {
            if (!_isCrouching)
                Crouch();
            else
                StandUp();
        }

        private void Crouch()
        {
            _characterController.height = _normalHeight * _playerCrouch.CrouchHeight;
            _characterController.center = new Vector3(0, _normalCenter.y * _playerCrouch.CrouchHeight, 0);
            _camera.position = new Vector3(_camera.position.x, _cameraPosition.y * _playerCrouch.CrouchHeight, _camera.position.z);
                
            _isCrouching = true;
        }

        private void StandUp()
        {
            _characterController.height = _normalHeight;
            _characterController.center = _normalCenter;
            _camera.position = new Vector3(_camera.position.x, _cameraPosition.y, _camera.position.z);
                    
            _isCrouching = false;
            
            
        }
    }
}