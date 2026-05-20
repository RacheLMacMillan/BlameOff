using System;
using UnityEngine;
using Core.Scripts.Player.Input;

namespace Core.Scripts.Player.Movement
{
    public class PlayerCrouch : MonoBehaviour
    {
        private const int MinCrouchHeight = 0;
        private const int MaxCrouchHeight = 1;
        private const int MinCrouchingSpeed = 1;
        private const int MaxCrouchingSpeed = 10;
        
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
        
        [Header("Multi")] // DO NOT CHANGE THIS FIELDS, USE THEIR 'PROPERTY' VERSIONS INSTEAD
        [SerializeField, Range(MinCrouchHeight, MaxCrouchHeight)] private float _crouchHeightMultiplier = 0.6f;
        [SerializeField, Range(MinCrouchingSpeed,MaxCrouchingSpeed)] private float _crouchSpeedMultiplier = 5f;
        
        [Header("Stand state")]
        [field: SerializeField] public float StandHeight { get; private set; }
        [field: SerializeField] public Vector3 StandCenter { get; private set; }

        [Header("Crouch state")] 
        [field: SerializeField] public float CrouchingHeight { get; private set; }
        [field: SerializeField] public Vector3 CrouchingCenter { get; private set; }

        public Action OnPlayerCrouched;
        public Action OnPlayerSandedUp;
        
        private PlayerInput _playerInput;
        private PlayerCharacterHeightController _playerCharacterHeightController;
        
        public bool IsCrouching { get; private set; }
        
        [SerializeField] private bool _isDebugging = false;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCharacterHeightController =  GetComponent<PlayerCharacterHeightController>();
        }

        private void OnEnable() => _playerInput.OnCrouchInputted += SwitchCrouch;
        private void OnDisable() => _playerInput.OnCrouchInputted -= SwitchCrouch;

        private void SwitchCrouch()
        {
            if (!IsCrouching)
                Crouch();
            else
                StandUp();
        }

        private void Crouch()
        {
            OnPlayerCrouched?.Invoke();
            
            IsCrouching = true;

            if (_isDebugging)
                Debug.Log("Player is crouching");
        }
        
        private void StandUp()
        {
            OnPlayerSandedUp?.Invoke();
            
            IsCrouching = false;
            
            if (_isDebugging)
                Debug.Log("Player is standing");
        }
    }
}