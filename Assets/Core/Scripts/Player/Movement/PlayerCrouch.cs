using System;
using UnityEngine;
using Core.Scripts.Player.Input;

namespace Core.Scripts.Player.Movement
{
    public class PlayerCrouch : MonoBehaviour
    {
        [Range(0,1)] public float CrouchHeight;
        [Range(1,10)] public float CrouchSpeed;

        public Action OnPlayerCrouched;
        
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.OnCrouchInputted += Crouch;
        }

        private void OnDisable()
        {
            _playerInput.OnCrouchInputted -= Crouch;
        }

        private void Crouch()
        {
            OnPlayerCrouched?.Invoke();
        }
    }
}
