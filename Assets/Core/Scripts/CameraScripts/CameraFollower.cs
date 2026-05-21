using System;
using Core.Scripts.Player.Movement;
using UnityEngine;

namespace Core.Scripts.CameraScripts
{
    public class CameraFollower : MonoBehaviour
    {   
        
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private Vector3 _playerStandingOffset;
        [SerializeField] private Vector3 _playerCrouchingOffset;
        [SerializeField] private Vector3 _playerLayingOffset;
        
        [SerializeField] private PlayerLooker _playerLooker;
        [SerializeField] private PlayerCrouch _playerCrouch;

        [SerializeField] private bool _isDebugging;
        
        private void Awake()
        {
            _playerLooker = GetComponentInParent<PlayerLooker>();
            _playerCrouch = GetComponentInParent<PlayerCrouch>();
        }

        private void OnEnable()
        {
            _playerLooker.OnPlayerLooked += FollowPlayerLook;
            _playerCrouch.OnPlayerSandedUp += GetUpCamera;
            _playerCrouch.OnPlayerCrouched += GetDownCamera;
        }

        private void OnDisable()
        {
            _playerLooker.OnPlayerLooked -= FollowPlayerLook;
            _playerCrouch.OnPlayerSandedUp -= GetUpCamera;
            _playerCrouch.OnPlayerCrouched -= GetDownCamera;
            
        }

        private void Update()
        {
            transform.position = _playerCrouch.transform.position + _cameraOffset;
        }

        private void FollowPlayerLook(Vector3 playerLookedByX, Quaternion playerLookedByY)
        {
            transform.localRotation = playerLookedByY;
        }

        private void GetUpCamera()
        {
            _cameraOffset = _playerStandingOffset;

            if (_isDebugging)
                Debug.Log("Player is standing and camera got up");
        }
        
        private void GetDownCamera()
        {
            _cameraOffset = _playerCrouchingOffset;

            if (_isDebugging)
                Debug.Log("Player is crouching and camera got down");
        }

        private void LieDownCamera()
        {
            _cameraOffset = _playerLayingOffset;

            if (_isDebugging)
                Debug.Log("Player is laying and camera lie down");
        }
    }
}