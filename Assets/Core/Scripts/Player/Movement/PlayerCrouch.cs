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
        
        [SerializeField] private bool _isDebugging = false;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable() => _playerInput.OnCrouchInputted += Crouch;
        private void OnDisable() => _playerInput.OnCrouchInputted -= Crouch;

        private void Crouch()
        {
            OnPlayerCrouched?.Invoke();
        }
    }
}