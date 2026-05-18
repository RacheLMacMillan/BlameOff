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

        public float Height => _characterController.height;
        public Vector3 Center => _characterController.center;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerCrouch = GetComponent<PlayerCrouch>();
        }

        // private void OnEnable() => _playerCrouch.OnPlayerCrouched += SwitchCrouching;
        // private void OnDisable() => _playerCrouch.OnPlayerCrouched -= SwitchCrouching;

    }
}