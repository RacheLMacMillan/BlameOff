using System;
using UnityEngine;
using Core.Scripts.Player.Input;

namespace Core.Scripts.Player.Movement
{
    public class PlayerCrouch : MonoBehaviour
    {
        public float CrouchHeightMultiplier
        {
            get => _crouchHeightMultiplier;
            private set => _crouchHeightMultiplier = Mathf.Clamp(value, MinCrouchHeight, MaxCrouchHeight);
        }
        public float CrouchSpeedMultiplier
        {
            get => _crouchSpeedMultiplier;
            private set => _crouchSpeedMultiplier = Mathf.Clamp(value, MinCrouchingSpeed, MaxCrouchingSpeed);
        }

        public Action<bool> OnPlayerCrouchedChanged;
        
        private bool _isCrouching;
        
        private PlayerInput _playerInput;
        private PlayerCharacterHeightController _playerCharacterHeightController;
        
        private float _defaultHeight;
        private Vector3 _defaultCenter;

        // DO NOT CHANGE THIS FIELDS, USE THEIR 'PROPERTY' VERSIONS INSTEAD
        [SerializeField, Range(MinCrouchHeight, MaxCrouchHeight)] private float _crouchHeightMultiplier = 0.6f;
        [SerializeField, Range(MinCrouchingSpeed,MaxCrouchingSpeed)] private float _crouchSpeedMultiplier = 5f;
        
        private const int MinCrouchHeight = 0;
        private const int MaxCrouchHeight = 1;
        private const int MinCrouchingSpeed = 1;
        private const int MaxCrouchingSpeed = 10;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCharacterHeightController =  GetComponent<PlayerCharacterHeightController>();
            
            _defaultHeight = _playerCharacterHeightController.Height;
            _defaultCenter = _playerCharacterHeightController.Center;
        }

        private void OnEnable() => _playerInput.OnCrouchInputted += SwitchCrouch;
        private void OnDisable() => _playerInput.OnCrouchInputted -= SwitchCrouch;

        private void SwitchCrouch()
        {
            if (!_isCrouching)
                Crouch();
            else
                StandUp();
        }

        private void Crouch()
        {
            // float height = _defaultHeight * _playerCrouch.CrouchHeight;
            // float center = _defaultCenter.y * _playerCrouch.CrouchHeight;
                
            _isCrouching = true;
        }
        
        private void StandUp()
        {
            // float height = _defaultHeight;
            // Vector3 center = _defaultCenter;
            
            _isCrouching = false;
        }
    }
}
