using System;
using UnityEngine;

namespace FPS
{
    public class PlayerCrouch : MonoBehaviour
    {
        [Header("Crouch state")] 
        [field: SerializeField] public float CrouchingHeight { get; private set; }
        [field: SerializeField] public Vector3 CrouchingCenter { get; private set; }

        public Action OnPlayerCrouched;
        
        private PlayerInput _playerInput;
        private IsAbleToCrouchChecker _isAbleToCrouchChecker;
        
        [SerializeField] private bool _isDebugging = false;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _isAbleToCrouchChecker = GetComponent<IsAbleToCrouchChecker>();
        }

        private void OnEnable() => _playerInput.OnCrouchInputted += Crouch;
        private void OnDisable() => _playerInput.OnCrouchInputted -= Crouch;

        private void Crouch()
        {
            if (_isAbleToCrouchChecker.IsPlayerAbleToCrouch == false)
                throw new ArgumentOutOfRangeException("Player isn't able to crouch here");

            OnPlayerCrouched?.Invoke();
            
            if (_isDebugging)
                Debug.Log("Player crouched");
        }
    }
}