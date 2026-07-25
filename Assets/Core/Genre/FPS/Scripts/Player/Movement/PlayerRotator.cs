using UnityEngine;

namespace FPS
{
    public class PlayerRotator : MonoBehaviour
    {
        private PlayerLooker _playerLooker;

        private void Awake()
        {
            _playerLooker =  GetComponent<PlayerLooker>();
        }

        private void OnEnable() => _playerLooker.OnPlayerLooked += (x,y) => RotatePlayer(x);
        private void OnDisable() => _playerLooker.OnPlayerLooked -= (x,y) => RotatePlayer(x);

        private void RotatePlayer(Vector3 playerLookedByX)
        {
			transform.Rotate(playerLookedByX);
        }
    }
}