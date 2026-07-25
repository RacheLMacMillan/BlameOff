using UnityEngine;

namespace FPS
{
    public class CameraFollower : MonoBehaviour
    {   
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private Vector3 _playerStandingOffset;
        [SerializeField] private Vector3 _playerCrouchingOffset;
        [SerializeField] private Vector3 _playerLayingOffset;

        [SerializeField] private PlayerLooker _playerLooker;
        [SerializeField] private PlayerCharacterHeightController _playerCharacterHeightController;
        [SerializeField] private PlayerCrouch _playerCrouch;
        [SerializeField] private PlayerLieDowner _playerLieDowner;

        [SerializeField] private bool _isDebugging;
        
        private void Awake()
        {
            _playerLooker = GetComponentInParent<PlayerLooker>();
            
            _playerCharacterHeightController = GetComponentInParent<PlayerCharacterHeightController>();
            _playerCrouch = GetComponentInParent<PlayerCrouch>();
            _playerLieDowner = GetComponentInParent<PlayerLieDowner>();
        }

        private void OnEnable()
        {
            _playerLooker.OnPlayerLooked += FollowPlayerLook;
            
            _playerCharacterHeightController.OnPlayerSandedUp += GetUpCamera;
            _playerCrouch.OnPlayerCrouched += GetDownCamera;
            _playerLieDowner.OnPlayerLayDown += LieDownCamera;
        }

        private void OnDisable()
        {
            _playerLooker.OnPlayerLooked -= FollowPlayerLook;
            
            _playerCharacterHeightController.OnPlayerSandedUp -= GetUpCamera;
            _playerCrouch.OnPlayerCrouched -= GetDownCamera;
            _playerLieDowner.OnPlayerLayDown -= LieDownCamera;
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