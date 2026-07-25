using System;
using UnityEngine;

namespace FPS
{
    public class PlayerLieDowner : MonoBehaviour
    {
        [field: SerializeField] public float LayingHeight { get; private set; } 
        [field: SerializeField] public Vector3 LayingCenter { get; private set; } 

        public Action OnPlayerLayDown;
        
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.OnLieDownInputted += Liedown;
        }

        private void OnDisable()
        {
            _playerInput.OnLieDownInputted -= Liedown;
        }

        private void Liedown()
        {
            OnPlayerLayDown?.Invoke();
        }
    }
}