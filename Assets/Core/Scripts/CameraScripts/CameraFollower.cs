using Core.Scripts.Player.Movement;
using UnityEngine;

namespace Core.Scripts.CameraScripts
{
    public class CameraFollower : MonoBehaviour
    {   
        [SerializeField] private Vector3 _cameraOffset;
        
        [SerializeField] private PlayerLooker _playerLooker;

        private void Awake()
        {
            _playerLooker = GetComponentInParent<PlayerLooker>();
        }

        private void OnEnable() => _playerLooker.PlayerLooked += FollowPlayerLook;
        private void OnDisable() => _playerLooker.PlayerLooked -= FollowPlayerLook;
        
        private void FollowPlayerLook(Vector3 playerLookedByX, Quaternion playerLookedByY)
        {
            transform.localRotation = playerLookedByY;
        }
    }
}