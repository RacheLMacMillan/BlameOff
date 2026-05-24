using System;
using UnityEngine;
using Core.Scripts.Player.Input;

namespace Core.Scripts.Player.Movement
{
    public class PlayerCrouch : MonoBehaviour
    {
        [Header("Crouch state")] 
        [field: SerializeField] public float CrouchingHeight { get; private set; }
        [field: SerializeField] public Vector3 CrouchingCenter { get; private set; }

        public Action OnPlayerCrouched;
        
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
            OnPlayerCrouched?.Invoke();
        }
    }
}